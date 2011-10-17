using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PubTricks.Web.Models;

namespace PubTricks.Web.Areas.Admin.Controllers
{
    public class TricksController : Controller
    {
        dynamic _tricksTable;
        public TricksController() {
            _tricksTable = new Tricks();
        }

        [Authorize(Roles = "Administrator")]
        public ActionResult Index()
        {
            var x = TempData["thing"];
            return View(_tricksTable.All());
        }

        [Authorize(Roles = "Administrator")]
        public ActionResult Details(int id)
        {
            return View(_tricksTable.FindBy(ID: id, schema: true));
        }

        [Authorize(Roles = "Administrator")]
        public ActionResult Create()
        {
            return View(_tricksTable.Prototype);
        } 

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public ActionResult Create(FormCollection collection)
        {
            var itemToCreate = _tricksTable.CreateFrom(collection);

            //convert from NZ to US dateformat
            //if DateCreated actually exists coming in
            //if not it will be caught in validation and set to now
            try {
                string t = itemToCreate.DateCreated;
                string[] words = t.Split('/');
                string day = words[0];
                string month = words[1];
                string year = words[2];
                string dateInUS = month + "/" + day + "/" + year;

                itemToCreate.DateCreated = dateInUS;
            }
            catch { }


            try
            {
                //validation enforced on model (as an override on Massive)
                var expandoNewlyCreatedTrick = _tricksTable.Insert(itemToCreate);

                //pass back newly created trick so that tests can make sure data is right
                TempData["newlyCreatedThing"] = expandoNewlyCreatedTrick;
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["Error"] = "There was an error adding the trick: "+ ex.Message;
                return View(itemToCreate);
            }
        }

        [Authorize(Roles = "Administrator")]
        public ActionResult Edit(int id)
        {
            var model = _tricksTable.Get(ID: id);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public ActionResult Edit(int id, FormCollection collection)
        {
            var itemToUpdate = _tricksTable.CreateFrom(collection);

            //convert from NZ to US dateformat
            //if DateCreated actually exists coming in
            //if not it will be caught in validation and set to now
            try {
                string t = itemToUpdate.DateCreated;
                string[] words = t.Split('/');
                string day = words[0];
                string month = words[1];
                string year = words[2];
                string dateInUS = month + "/" + day + "/" + year;

                itemToUpdate.DateCreated = dateInUS;
            }
            catch { }

            try
            {
                _tricksTable.Update(itemToUpdate, id); 
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["Error"] = "There was an error editing this trick: " + ex.Message;
                return View(itemToUpdate);
            }
        }

        public ActionResult Delete(int id)
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                _tricksTable.Delete(id);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["Error"] = "There was a problem deleting this record: " + ex.Message;
                return View("Index");
            }
        }
    }
}
