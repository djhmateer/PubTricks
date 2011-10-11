using System;
using System.Dynamic;
using System.Linq;
using Massive;

namespace PubTricks.Web.Models {
    public class Tricks : DynamicModel {
        //DynamicModel _tbl;
        //dynamic _tbl;

        public Tricks() : base("PubTricks", "Tricks", "ID") {
            //_tbl = new DynamicModel("PubTricks", "Tricks", "ID");
        }

        public dynamic AddTrick(string name, string description, string videourl) {
            dynamic result = new ExpandoObject();
            result.Success = true;
            //is name of a trick is in the db already?
            //var trickFromDB = _tbl.All(where: "WHERE Name=@0", args: name);
            var trickFromDB = this.All(where: "WHERE Name=@0", args: name);
            if (trickFromDB.Count() > 0) {
                result.Message = "Duplicate names of tricks not allowed";
                result.Success = false;
                return result;
            }

            //is url in db already?
            //trickFromDB = _tbl.All(where: "WHERE VideoURL=@0", args: videourl);
            trickFromDB = this.All(where: "WHERE VideoURL=@0", args: videourl);
            if (trickFromDB.Count() > 0) {
                result.Message = "Duplicate VideoURL of tricks not allowed";
                result.Success = false;
                return result;
            }

            //is description long enough?
            if (description.Length <= 5) {
                result.Message = "Need more than 5 characters in the description";
                result.Success = false;
                return result;
            }

            try {
                //_tbl.Insert(new { Name = name, Description = description, VideoURL = videourl });
                this.Insert(new { Name = name, Description = description, VideoURL = videourl });
            }
            catch (Exception ex) {
                result.Success = false;
                result.Message = "Database problem";
            }
            return result;
        }
    }
}