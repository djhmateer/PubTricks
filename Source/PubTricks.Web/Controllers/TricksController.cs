using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PubTricks.Web.Models;
using System.Linq;
using System.Collections;

namespace PubTricks.Web.Controllers
{
    public class TricksController : Controller
    {
        dynamic _tricksTable;
        public TricksController() {
            _tricksTable = new Tricks();
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

            var model = _tricksTable.Get(ID: id);
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
            if (id == 0) {
                GetIDFromTrickName(ref id, ref trickName);
            }

            var trickToAddVoteTo = _tricksTable.Get(ID: id);
            trickToAddVoteTo.Votes += 1;

            //add a cookie to the users browser for this trick
            string cookieName = "Trick_" + id.ToString();
            HttpCookie cookie = new HttpCookie(cookieName);
            cookie.Value = DateTime.Now.ToString();
            cookie.Expires = DateTime.Now.AddYears(1);

            this.ControllerContext.HttpContext.Response.Cookies.Add(cookie);

            try {
                _tricksTable.Update(trickToAddVoteTo, id);
                return RedirectToAction("Details", id);
            }
            catch (Exception ex) {
                TempData["Error"] = "There was a problem updating the like: " + ex.Message;
                return View();
            }
        }
    }
}
