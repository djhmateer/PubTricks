using System;
using System.Dynamic;
using System.Linq;
using Massive;
using System.Diagnostics;

namespace PubTricks.Web.Models {
    public class Tricks : DynamicModel {

        public Tricks() : base("PubTricks", "Tricks", "ID") {}

        public dynamic AddTrick(string name, string description, string videourl, string datecreated = "",
                                int votes = 0, string thumbnail = "", string longdescription = "", string videosolutionurl = "") {
            dynamic result = new ExpandoObject();
            result.Message = "";
            result.Success = true;

            //validate data again - should have been caught already by HTML5 validators
            if (name == "") {
                result.Message = "Name of trick needs to be something";
                result.Success = false;
                return result;
            }

            //is name of a trick is in the db already?
            var trickFromDB = this.All(where: "WHERE Name=@0", args: name);
            if (trickFromDB.Count() > 0) {
                result.Message = "Duplicate names of tricks not allowed";
                result.Success = false;
                return result;
            }

            //is videourl in db already?
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
                this.Insert(new { Name = name, Description = description, VideoURL = videourl, DateCreated = datecreated,
                                    Votes = votes, Thumbnail = thumbnail, LongDescription = longdescription, VideoSolutionURL = videosolutionurl});
            }
            catch (Exception ex) {
                result.Success = false;
                result.Message = "Database problem: " + ex.Message;
            }
            return result;
        }

        public override void Validate(dynamic item) {
            this.ValidatesPresenceOf(item.Name, "Name is required");
            //this.ValidatesPresenceOf(item.Price, "Price is required");
            //this.ValidateIsCurrency(item.Price, "Should be a number");
            ////price needs to be > 0
            //if (decimal.Parse(item.Price) <= 0) {
            //    Errors.Add("Price has to be more than 0 - can't give this stuff away!");
            //}
        }

        public dynamic GetTenTricksNewestFirst() {
            var data = this.Query("SELECT TOP(10) * FROM Tricks ORDER BY DateCreated DESC");
            return data;
        }

        public dynamic GetTenTricksMostPopularFirst() {
            var data = this.Query("SELECT TOP(10) * FROM Tricks ORDER BY Votes DESC");
            return data;
        }
    }
}