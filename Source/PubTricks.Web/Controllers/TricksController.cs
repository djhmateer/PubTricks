using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PubTricks.Web.Models;
using System.Linq;
using System.Collections;
using System.Dynamic;

namespace PubTricks.Web.Controllers
{
    public class TricksController : Controller
    {
        dynamic _tricksTable;
        dynamic _commentsTable;
        dynamic _tricksCategoriesTable;

        public TricksController() {
            _tricksTable = new Tricks();
            _commentsTable = new Comments();
            _tricksCategoriesTable = new TricksCategories();
        }
       
        //
        // /Tricks/Details/5 
        // /Tricks/The-Pen-Trick
        public ActionResult Details(int id = 0, string trickName = "")
        {
            //if we have been passed /Tricks/Pen-Trick find the id
        
            if (id == 0) {
                GetIDFromTrickName(ref id, ref trickName);
            }

            //see if there is a cookie in the users browser for this trick (ie have they liked it before)
            string cookieValue = "";
            string cookieName = "Trick_" + id.ToString();
            try {
                if (this.ControllerContext.HttpContext.Request.Cookies.AllKeys.Contains(cookieName)) {
                    cookieValue = cookieName + ": " + this.ControllerContext.HttpContext.Request.Cookies[cookieName].Value;
                }
                ViewData["CookieStuffToDisplay"] = cookieValue;
            }
            catch {
                //from test harness - refactor this.
            }

            dynamic model = new ExpandoObject();

            var trickData = _tricksTable.Get(ID: id);
            model.TrickData = trickData;

            var trickComments = _commentsTable.Query("SELECT * FROM Comments WHERE TrickID = " + id);
            model.TrickComments = trickComments;


            //trickcategories
            var trickCategories = _tricksCategoriesTable.Query("SELECT Categories.Name as CategoryName FROM TricksCategories INNER JOIN " +
                                                                "Categories ON TricksCategories.CategoryID = Categories.ID WHERE TricksCategories.TrickID = " + id);
            model.TrickCategories = trickCategories;


            return View(model);
        }

        private void GetIDFromTrickName(ref int id, ref string trickName) {
            trickName = trickName.ToLower();

            trickName = trickName.Replace("-", " ");

            string sql = "SELECT ID from Tricks WHERE Name = '" + trickName + "'";
            var result = _tricksTable.Query(sql);

            //refactor - tried ToList() and FirstOrDefault() but this seems to be only way
            int x = 0;
            foreach (var item in result) {
                x = item.ID;
            }
            id = x;
        }

        //when like button is pressed
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Details(FormCollection collection, int id = 0, string trickName = "") {
            if (id == 0)
                GetIDFromTrickName(ref id, ref trickName);

            var trickToAddVoteTo = _tricksTable.Get(ID: id);
            trickToAddVoteTo.Votes += 1;

            //todo refactor this - understand why I have to cast to a string to make massive work?
            //it goes into the db fine as ints
            trickToAddVoteTo.Votes = Convert.ToString(trickToAddVoteTo.Votes);
            trickToAddVoteTo.ID = Convert.ToString(trickToAddVoteTo.ID);

            //add a cookie to the users browser for this trick
            string cookieName = "Trick_" + id.ToString();
            HttpCookie cookie = new HttpCookie(cookieName);
            cookie.Value = DateTime.Now.ToString();
            cookie.Expires = DateTime.Now.AddYears(1);

            //hack to get testing working
            try {
                this.ControllerContext.HttpContext.Response.Cookies.Add(cookie);
            }
            catch { }

            //trap any Massive errors
            try {
                var y =_tricksTable.Update(trickToAddVoteTo, id);
                return RedirectToAction("Details", id);
            }
            catch (Exception ex) {
                TempData["Error"] = "There was a problem updating the like: " + ex.Message;
                return View();
            }
        }
    }
}
