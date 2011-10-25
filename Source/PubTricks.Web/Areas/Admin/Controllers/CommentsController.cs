using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PubTricks.Web.Models;
using System.Dynamic;
using System.Diagnostics;

namespace PubTricks.Web.Areas.Admin.Controllers
{
    public class CommentsController : Controller
    {
        dynamic _commentsTable;
        dynamic _tricksTable;

        public CommentsController() {
            _commentsTable = new Comments();
            _tricksTable = new Tricks();
        }

        [Authorize(Roles = "Administrator")]
        public ActionResult Index()
        {
            //create a viewmodel so we can see the name of the trick
            var x = _commentsTable.GetCommentsAndTrickNameForIndex();
            //var x = _commentsTable.All();
            return View(x);
        }

        [Authorize(Roles = "Administrator")]
        public ActionResult Details(int id)
        {
            return View(_commentsTable.FindBy(ID: id, schema: true));
        }

        [Authorize(Roles = "Administrator")]
        public ActionResult Create()
        {
            var data = _commentsTable.Prototype;
            dynamic model = new ExpandoObject();
            model.Data = data;

            List<dynamic> dropDownListOfTricks = GetDropDownListOfTricks();
            model.DropDownListOfTricks = dropDownListOfTricks;

            return View(model);
        } 

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public ActionResult Create(FormCollection collection)
        {
            var itemToCreate = _commentsTable.CreateFrom(collection);

            DateFromNZToSQL(itemToCreate);
            try
            {
                 _commentsTable.Insert(itemToCreate);
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
            var data = _commentsTable.Get(ID: id);
            dynamic model = new ExpandoObject();
            model.Data = data;

            List<dynamic> dropDownListOfTricks = GetDropDownListOfTricks();
            model.DropDownListOfTricks = dropDownListOfTricks;

            return View(model);
        }

        private List<dynamic> GetDropDownListOfTricks() {
            var dropDownListOfTricks = _tricksTable.Query("SELECT ID as TrickID, Name as TrickName FROM Tricks");
            ////enumerate the list and populate
            List<dynamic> listOfx = new List<dynamic>();
            foreach (var item in dropDownListOfTricks) {
                dynamic x = new ExpandoObject();
                x.TrickID = item.TrickID;
                x.TrickName = item.TrickName;
                listOfx.Add(x);
            }
            return listOfx;
        }

       

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public ActionResult Edit(int id, FormCollection collection)
        {
            var itemToUpdate = _commentsTable.CreateFrom(collection);

            DateFromNZToSQL(itemToUpdate);
            try
            {
                _commentsTable.Update(itemToUpdate, id); 
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
            var model = _commentsTable.Get(ID: id);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                _commentsTable.Delete(id);
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
