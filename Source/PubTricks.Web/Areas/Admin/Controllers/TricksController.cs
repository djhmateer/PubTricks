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

            DateFromNZToSQL(itemToCreate);
            try
            {
                 _tricksTable.Insert(itemToCreate);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["Error"] = "There was an error adding the trick: "+ ex.Message;
                DateFromSQLToNZ(itemToCreate);
                return View(itemToCreate);
            }
        }

        private static void DateFromNZToSQL(dynamic itemToCreate) {
            try {
                //convert nz to sql date
                //nz is dd/mm/yyyy   sql is yyyy-mm-dd
                string x = itemToCreate.DateCreated;
                string[] bits = x.Split('/');
                var day = bits[0];
                var month = bits[1];
                var year = bits[2];
                var sqlDate = year + "-" + month + "-" + day;
                itemToCreate.DateCreated = sqlDate;
            }
            catch { }
        }

        private static void DateFromSQLToNZ(dynamic itemToCreate) {
            try {
                string x = itemToCreate.DateCreated;
                string[] bits = x.Split('-');
                var day = bits[2];
                var month = bits[1];
                var year = bits[0];
                var nzDate = day + "/" + month + "/" + year;
                itemToCreate.DateCreated = nzDate;
            }
            catch { }
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

            DateFromNZToSQL(itemToUpdate);
            try
            {
                _tricksTable.Update(itemToUpdate, id); 
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["Error"] = "There was an error editing this trick: " + ex.Message;
                DateFromSQLToNZ(itemToUpdate);
                return View(itemToUpdate);
            }
        }

        [Authorize(Roles = "Administrator")]
        public ActionResult Delete(int id)
        {
            var model = _tricksTable.Get(ID: id);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
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
