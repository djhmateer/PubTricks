using System;
using System.Dynamic;
using System.Linq;
using Massive;
using System.Diagnostics;
using System.Collections.Generic;

namespace PubTricks.Web.Models {
    public class Tricks : DynamicModel {

        public Tricks() : base("PubTricks", "Tricks", "ID") { }

        public override void Validate(dynamic item) {
            //for testing purposes create dynamic properties if they don't exist
            //refactor into test code
            var dict = (IDictionary<String, object>)item;
            if (!dict.ContainsKey("VideoURL"))
                item.VideoURL = "";

            //we came from edit or delete
            bool editOrDelMode = false;
            if (dict.ContainsKey("ID"))
                editOrDelMode = true; 

            //name
            this.ValidatesPresenceOf(item.Name, "Name is required");

            //is name of a trick is in the db already
            var trickFromDB = this.All(where: "WHERE Name=@0", args: item.Name);
            if (Enumerable.Count(trickFromDB) > 0 && !editOrDelMode) {
                this.Errors.Add("Duplicate names of tricks not allowed");
            }

            //is videourl in db already
            trickFromDB = this.All(where: "WHERE VideoURL=@0", args: item.VideoURL);
            //duplicate empty videoURLs are allowed
            if (item.VideoURL != "") {
                if (Enumerable.Count(trickFromDB) > 0 && !editOrDelMode) {
                    this.Errors.Add("Duplicate VideoURL of tricks not allowed");
                }
            }

            //description
            this.ValidatesPresenceOf(item.Description, "Description is required");

            if (item.Description.Length <= 5) {
                this.Errors.Add("Need more than 5 characters in the description");
            }

            //votes validation and default
            bool keepChecking = true;
            //default number of votes if an empty string passed
            if (item.Votes == "") {
                item.Votes = 0;
                keepChecking = false;
            }

            if (keepChecking) {
                int dummy;
                if (Int32.TryParse(item.Votes, out dummy)) {
                    //a number was passed in - good
                    item.Votes = dummy;
                }
                else {
                    //not a number or an empty string was passed in - bad.
                    this.ValidatesNumericalityOf(item.Votes, "Votes should be a number");
                }
            }

            //DATE Validation and put in a default if none provided?

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