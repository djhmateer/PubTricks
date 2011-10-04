using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Massive;
using System.Dynamic;

namespace PubTricks.Web.Models {
    public class Trick {
        DynamicModel _tbl;

        public Trick() {
            _tbl = new DynamicModel("PubTricks", "Tricks", "ID");
        }

        public dynamic AddTrick(string name, string description, string videourl) {
            dynamic result = new ExpandoObject();
            result.Success = true;
            //name of a trick is in the db already
            var trickFromDB = _tbl.All(where: "WHERE Name=@0", args: name);
            if (trickFromDB.Count() > 0) {
                result.Message = "Duplicate names of tricks not allowed";
                result.Success = false;
                return result;
            }

            //url in db already
            trickFromDB = _tbl.All(where: "WHERE VideoURL=@0", args: videourl);
            if (trickFromDB.Count() > 0) {
                result.Message = "Duplicate VideoURL of tricks not allowed";
                result.Success = false;
                return result;
            }

            if (description.Length <= 5) {
                result.Message = "Need more than 5 characters in the description";
                result.Success = false;
                return result;
            }

            try {
                _tbl.Insert(new { Name = name, Description = description, VideoURL = videourl });
            }
            catch (Exception ex) {
                result.Success = false;
                result.Message = "Database problem";
            }
            return result;
        }
    }
}