using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Massive;

namespace PubTricks.Web.Models {
    public class Categories : DynamicModel {

        public Categories() : base("PubTricks", "Categories", "ID") { }

        //public override void Validate(dynamic item) {
        //    var dict = (IDictionary<String, object>)item;

        //    //we came from edit / or like button is pressed which increments and updates which calls this.
        //    bool editMode = false;
        //    if (dict.ContainsKey("ID"))
        //        editMode = true;

        //    //name
        //    this.ValidatesPresenceOf(item.Name, "Name is required");

        //    //duplicate name check
        //    //are we in editMode and name still the same
        //    bool isNameTheSame = true;
        //    if (editMode) {
        //        dynamic trickFromDBByID = new ExpandoObject();

        //        var trickFromDBList = this.All(where: "WHERE ID=@0", args: item.ID);
        //        //get the only one
        //        foreach (var item2 in trickFromDBList) {
        //            trickFromDBByID = item2;
        //        }
        //        if (trickFromDBByID.Name != item.Name)
        //            isNameTheSame = false;
        //    }

        //    //if editmode, and name the same, do nothing, otherwise check not a duplicate
        //    if (editMode && isNameTheSame) { }
        //    else {
        //        //is name of a trick is in the db already
        //        var trickFromDB = this.All(where: "WHERE Name=@0", args: item.Name);
        //        if (Enumerable.Count(trickFromDB) > 0) {
        //            this.Errors.Add("Duplicate names of tricks not allowed");
        //        }
        //    }


        //    //duplicate videourl check
        //    //are we in editMode and video url is still the same
        //    bool isVideoURLSame = true;
        //    if (editMode) {
        //        dynamic trickFromDBByID = new ExpandoObject();
        //        var trickFromDBList = this.All(where: "WHERE ID=@0", args: item.ID);
        //        //get the only one
        //        foreach (var item2 in trickFromDBList) {
        //            trickFromDBByID = item2;
        //        }
        //        if (trickFromDBByID.VideoURL != item.VideoURL)
        //            isVideoURLSame = false;
        //    }

        //    //if editmode, and videourl the same, do nothing, otherwise check not a duplicate
        //    if (editMode && isVideoURLSame) { }
        //    else {
        //        var trickFromDB2 = this.All(where: "WHERE VideoURL=@0", args: item.VideoURL);
        //        //duplicate empty videoURLs are allowed
        //        if (item.VideoURL != "") {
        //            if (Enumerable.Count(trickFromDB2) > 0) {
        //                this.Errors.Add("Duplicate VideoURL of tricks not allowed");
        //            }
        //        }
        //    }

        //    //description
        //    this.ValidatesPresenceOf(item.Description, "Description is required");

        //    if (item.Description.Length <= 5) {
        //        this.Errors.Add("Need more than 5 characters in the description");
        //    }

        //    //votes validation and default
        //    bool keepChecking = true;
        //    //default number of votes if an empty string passed
        //    if (item.Votes == "") {
        //        item.Votes = 0;
        //        keepChecking = false;
        //    }

        //    if (keepChecking) {
        //        int dummy;
        //        if (Int32.TryParse(item.Votes, out dummy)) {
        //            //a number was passed in - good
        //            item.Votes = dummy;
        //        }
        //        else {
        //            //not a number or an empty string was passed in - bad.
        //            this.ValidatesNumericalityOf(item.Votes, "Votes should be a number");
        //        }
        //    }

        //    //date
        //    //fail if doesn't exist.  reflection another way to do this
        //    try {
        //        if (item.DateCreated == "")
        //            item.DateCreated = DateTime.Now;
        //    }
        //    catch { item.DateCreated = DateTime.Now; }

        //    //if (item.DateCreated == "")
        //    //    item.DateCreated = DateTime.Now;

        //    //this.ValidateIsCurrency(item.Price, "Should be a number");
        //    ////price needs to be > 0
        //    //if (decimal.Parse(item.Price) <= 0) {
        //    //    Errors.Add("Price has to be more than 0 - can't give this stuff away!");
        //    //}
        //}

        

        //public dynamic GetCommentsAndTrickNameForIndex() {
        //    var data = this.Query("SELECT Comments.ID, Comments.CommentText, Tricks.Name as TrickName FROM Comments " +
        //                          "INNER JOIN Tricks ON Comments.TrickID = Tricks.ID ");
        //    return data;
        //}

        //public dynamic GetTenTricksMostPopularFirst() {
        //    var data = this.Query("SELECT TOP(10) * FROM Tricks ORDER BY Votes DESC");
        //    return data;
        //}
    }
}