using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Dynamic;
using System.Data.Common;
using System.Configuration;
using System.Data;
using System.Collections.Specialized;

namespace Massive {
    public static class ObjectExtensions {
        public static dynamic RecordToExpando(this IDataReader rdr) {
            dynamic e = new ExpandoObject();
            var d = e as IDictionary<string, object>;
            for (int i = 0; i < rdr.FieldCount; i++)
                d.Add(rdr.GetName(i), DBNull.Value.Equals(rdr[i]) ? null : rdr[i]);
            return e;
        }

        public static void AddParams(this DbCommand cmd, params object[] args) {
            foreach (var item in args) {
                AddParam(cmd, item);
            }
        }
        public static void AddParam(this DbCommand cmd, object item) {
            var p = cmd.CreateParameter();
            p.ParameterName = string.Format("@{0}", cmd.Parameters.Count);
            if (item == null) {
                p.Value = DBNull.Value;
            }
            else {
                if (item.GetType() == typeof(Guid)) {
                    p.Value = item.ToString();
                    p.DbType = DbType.String;
                    p.Size = 4000;
                }
                else if (item.GetType() == typeof(ExpandoObject)) {
                    var d = (IDictionary<string, object>)item;
                    p.Value = d.Values.FirstOrDefault();
                }
                else {
                    p.Value = item;
                }
                if (item.GetType() == typeof(string))
                    p.Size = ((string)item).Length > 4000 ? -1 : 4000;
            }
            cmd.Parameters.Add(p);
        }

        public static dynamic ToExpando(this object o) {
            var result = new ExpandoObject();
            var d = result as IDictionary<string, object>; //work with the Expando as a Dictionary
            if (o.GetType() == typeof(ExpandoObject)) return o; //shouldn't have to... but just in case
            if (o.GetType() == typeof(NameValueCollection) || o.GetType().IsSubclassOf(typeof(NameValueCollection))) {
                var nv = (NameValueCollection)o;
                nv.Cast<string>().Select(key => new KeyValuePair<string, object>(key, nv[key])).ToList().ForEach(i => d.Add(i));
            }
            else {
                var props = o.GetType().GetProperties();
                foreach (var item in props) {
                    d.Add(item.Name, item.GetValue(o, null));
                }
            }
            return result;
        }
    }

    public class DynamicModel : DynamicObject {
        DbProviderFactory _factory;
        string _connectionString;
        public string TableName { get; set; }
        public string PrimaryKeyField { get; set; }

        public DynamicModel(string connectionStringName, string tableName, string primaryKeyField) {
            TableName = tableName;
            PrimaryKeyField = primaryKeyField;
            string _providerName = "System.Data.SqlClient";
            _factory = DbProviderFactories.GetFactory(_providerName);
            _connectionString = ConfigurationManager.ConnectionStrings[connectionStringName].ConnectionString;
        }

        public dynamic CreateFrom(NameValueCollection coll) {
            dynamic result = new ExpandoObject();
            var dc = (IDictionary<string, object>)result;
            var schema = Schema;
            //loop the collection, setting only what's in the Schema
            foreach (var item in coll.Keys) {
                var exists = schema.Any(x => x.COLUMN_NAME.ToLower() == item.ToString().ToLower());
                if (exists) {
                    var key = item.ToString();
                    var val = coll[key];
                    dc.Add(key, val);
                }
            }
            return result;
        }

        public dynamic Prototype {
            get {
                dynamic result = new ExpandoObject();
                var schema = Schema;
                foreach (dynamic column in schema) {
                    var dc = (IDictionary<string, object>)result;
                    dc.Add(column.COLUMN_NAME, DefaultValue(column));
                }
                result._Table = this;
                return result;
            }
        }
        public dynamic DefaultValue(dynamic column) {
            dynamic result = null;
            string def = column.COLUMN_DEFAULT;
            if (String.IsNullOrEmpty(def)) {
                result = null;
            }
            else if (def == "getdate()" || def == "(getdate())") {
                result = DateTime.Now.ToShortDateString();
            }
            else if (def == "newid()") {
                result = Guid.NewGuid().ToString();
            }
            else {
                result = def.Replace("(", "").Replace(")", "");
            }
            return result;
        }

        IEnumerable<dynamic> _schema;
        public IEnumerable<dynamic> Schema {
            get {
                if (_schema == null)
                    _schema = Query("SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = @0", TableName);
                return _schema;
            }
        }

        public IEnumerable<dynamic> All(string where = "", string orderBy = "", int limit = 0, string columns = "*", params object[] args) {
            string sql = BuildSelect(where, orderBy, limit);
            return Query(string.Format(sql, columns, TableName), args);
        }

        private static string BuildSelect(string where, string orderBy, int limit) {
            string sql = limit > 0 ? "SELECT TOP " + limit + " {0} FROM {1} " : "SELECT {0} FROM {1} ";
            if (!string.IsNullOrEmpty(where))
                sql += where.Trim().StartsWith("where", StringComparison.OrdinalIgnoreCase) ? where : "WHERE " + where;
            if (!String.IsNullOrEmpty(orderBy))
                sql += orderBy.Trim().StartsWith("order by", StringComparison.OrdinalIgnoreCase) ? orderBy : " ORDER BY " + orderBy;
            return sql;
        }

        public IEnumerable<dynamic> Query(string sql, params object[] args) {
            using (var conn = OpenConnection()) {
                var rdr = CreateCommand(sql, conn, args).ExecuteReader();
                while (rdr.Read()) {
                    yield return rdr.RecordToExpando(); ;
                }
            }
        }

        public IList<string> Errors = new List<string>();
        public dynamic Insert(object o) {
            var ex = o.ToExpando();
            if (!IsValid(ex)) {
                throw new InvalidOperationException("Can't insert: " + String.Join("; ", Errors.ToArray()));
            }
            using (dynamic conn = OpenConnection()) {
                var cmd = CreateInsertCommand(ex);
                cmd.Connection = conn;
                cmd.ExecuteNonQuery();
                cmd.CommandText = "SELECT @@IDENTITY as newID";
                ex.ID = cmd.ExecuteScalar();
            }
            return ex;

        }

        public virtual int Update(object o, object key) {
            var ex = o.ToExpando();
            if (!IsValid(ex)) {
                throw new InvalidOperationException("Can't Update: " + String.Join("; ", Errors.ToArray()));
            }
            var result = 0;
            result = Execute(CreateUpdateCommand(ex, key));
            return result;
        }

        public bool IsValid(dynamic item) {
            Errors.Clear();
            return Errors.Count == 0;
        }

        public DbCommand CreateInsertCommand(dynamic expando) {
            DbCommand result = null;
            var settings = (IDictionary<string, object>)expando;
            var sbKeys = new StringBuilder();
            var sbVals = new StringBuilder();
            var stub = "INSERT INTO {0} ({1}) \r\n VALUES ({2})";
            result = CreateCommand(stub, null);
            int counter = 0;
            foreach (var item in settings) {
                sbKeys.AppendFormat("{0},", item.Key);
                sbVals.AppendFormat("@{0},", counter.ToString());
                result.AddParam(item.Value);
                counter++;
            }
            if (counter > 0) {
                var keys = sbKeys.ToString().Substring(0, sbKeys.Length - 1);
                var vals = sbVals.ToString().Substring(0, sbVals.Length - 1);
                var sql = string.Format(stub, TableName, keys, vals);
                result.CommandText = sql;
            }
            else throw new InvalidOperationException("Can't parse this object to the database - there are no properties set");
            return result;
        }
        public DbCommand CreateUpdateCommand(dynamic expando, object key) {
            var settings = (IDictionary<string, object>)expando;
            var sbKeys = new StringBuilder();
            var stub = "UPDATE {0} SET {1} WHERE {2} = @{3}";
            var args = new List<object>();
            var result = CreateCommand(stub, null);
            int counter = 0;
            foreach (var item in settings) {
                var val = item.Value;
                if (!item.Key.Equals(PrimaryKeyField, StringComparison.OrdinalIgnoreCase) && item.Value != null) {
                    result.AddParam(val);
                    sbKeys.AppendFormat("{0} = @{1}, \r\n", item.Key, counter.ToString());
                    counter++;
                }
            }
            if (counter > 0) {
                //add the key
                result.AddParam(key);
                //strip the last commas
                var keys = sbKeys.ToString().Substring(0, sbKeys.Length - 4);
                result.CommandText = string.Format(stub, TableName, keys, PrimaryKeyField, counter);
            }
            else throw new InvalidOperationException("No parsable object was sent in - could not divine any name/value pairs");
            return result;
        }

        public object Scalar(string sql, params object[] args) {
            object result = null;
            using (var conn = OpenConnection()) {
                result = CreateCommand(sql, conn, args).ExecuteScalar();
            }
            return result;
        }

        public DbConnection OpenConnection() {
            var result = _factory.CreateConnection();
            result.ConnectionString = _connectionString;
            result.Open();
            return result;
        }

        DbCommand CreateCommand(string sql, DbConnection conn, params object[] args) {
            var result = _factory.CreateCommand();
            result.Connection = conn;
            result.CommandText = sql;
            if (args.Length > 0)
                result.AddParams(args);
            return result;
        }

        public int Delete(object key = null, string where = "", params object[] args) {
            var deleted = this.Single(key);
            var result = 0;

            result = Execute(CreateDeleteCommand(where: where, key: key, args: args));
            return result;
        }

        public virtual dynamic Single(object key, string columns = "*") {
            var sql = string.Format("SELECT {0} FROM {1} WHERE {2} = @0", columns, TableName, PrimaryKeyField);
            return Query(sql, key).FirstOrDefault();
        }

        public virtual int Execute(DbCommand command) {
            return Execute(new DbCommand[] { command });
        }

        public virtual int Execute(string sql, params object[] args) {
            return Execute(CreateCommand(sql, null, args));
        }

        public virtual int Execute(IEnumerable<DbCommand> commands) {
            var result = 0;
            using (var conn = OpenConnection()) {
                using (var tx = conn.BeginTransaction()) {
                    foreach (var cmd in commands) {
                        cmd.Connection = conn;
                        cmd.Transaction = tx;
                        result += cmd.ExecuteNonQuery();
                    }
                    tx.Commit();
                }
            }
            return result;
        }

        public virtual DbCommand CreateDeleteCommand(string where = "", object key = null, params object[] args) {
            var sql = string.Format("DELETE FROM {0} ", TableName);
            if (key != null) {
                sql += string.Format("WHERE {0}=@0", PrimaryKeyField);
                args = new object[] { key };
            }
            else if (!string.IsNullOrEmpty(where)) {
                sql += where.Trim().StartsWith("where", StringComparison.OrdinalIgnoreCase) ? where : "WHERE " + where;
            }
            return CreateCommand(sql, null, args);
        }


        public override bool TryInvokeMember(InvokeMemberBinder binder, object[] args, out object result) {
            //parse the method
            var constraints = new List<string>();
            var counter = 0;
            var info = binder.CallInfo;
            // accepting named args only... SKEET!
            if (info.ArgumentNames.Count != args.Length) {
                throw new InvalidOperationException("Please use named arguments for this type of query - the column name, orderby, columns, etc");
            }
            //first should be "FindBy, Last, Single, First"
            var op = binder.Name;
            var columns = " * ";
            string orderBy = string.Format(" ORDER BY {0}", PrimaryKeyField);
            string sql = "";
            string where = "";
            var whereArgs = new List<object>();

            //loop the named args - see if we have order, columns and constraints
            if (info.ArgumentNames.Count > 0) {

                for (int i = 0; i < args.Length; i++) {
                    var name = info.ArgumentNames[i].ToLower();
                    switch (name) {
                        case "orderby":
                            orderBy = " ORDER BY " + args[i];
                            break;
                        case "columns":
                            columns = args[i].ToString();
                            break;
                        default:
                            constraints.Add(string.Format(" {0} = @{1}", name, counter));
                            whereArgs.Add(args[i]);
                            counter++;
                            break;
                    }
                }
            }

            //Build the WHERE bits
            if (constraints.Count > 0) {
                where = " WHERE " + string.Join(" AND ", constraints.ToArray());
            }
            //probably a bit much here but... yeah this whole thing needs to be refactored...
            if (op.ToLower() == "count") {
                result = Scalar("SELECT COUNT(*) FROM " + TableName + where, whereArgs.ToArray());
            }
            else if (op.ToLower() == "sum") {
                result = Scalar("SELECT SUM(" + columns + ") FROM " + TableName + where, whereArgs.ToArray());
            }
            else if (op.ToLower() == "max") {
                result = Scalar("SELECT MAX(" + columns + ") FROM " + TableName + where, whereArgs.ToArray());
            }
            else if (op.ToLower() == "min") {
                result = Scalar("SELECT MIN(" + columns + ") FROM " + TableName + where, whereArgs.ToArray());
            }
            else if (op.ToLower() == "avg") {
                result = Scalar("SELECT AVG(" + columns + ") FROM " + TableName + where, whereArgs.ToArray());
            }
            else {

                //build the SQL
                sql = "SELECT TOP 1 " + columns + " FROM " + TableName + where;
                var justOne = op.StartsWith("First") || op.StartsWith("Last") || op.StartsWith("Get") || op.StartsWith("Single");

                //Be sure to sort by DESC on the PK (PK Sort is the default)
                if (op.StartsWith("Last")) {
                    orderBy = orderBy + " DESC ";
                }
                else {
                    //default to multiple
                    sql = "SELECT " + columns + " FROM " + TableName + where;
                }

                if (justOne) {
                    //return a single record
                    result = Query(sql + orderBy, whereArgs.ToArray()).FirstOrDefault();
                }
                else {
                    //return lots
                    result = Query(sql + orderBy, whereArgs.ToArray());
                }
            }
            return true;
        }
    }


}
