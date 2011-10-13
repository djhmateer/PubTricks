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
            try
            {
                //validation enforced on model (as an override on Massive)
                _tricksTable.Insert(itemToCreate);

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                var x = 1;
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
            try
            {
                _tricksTable.Update(itemToUpdate, id); 
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["Error"] = "There was a problem editing this record: " + ex.Message;
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
