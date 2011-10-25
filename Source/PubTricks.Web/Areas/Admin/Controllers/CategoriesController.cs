using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PubTricks.Web.Models;
using System.Dynamic;

namespace PubTricks.Web.Areas.Admin.Controllers
{
    public class CategoriesController : Controller
    {
        dynamic _categoriesTable;
        dynamic _tricksTable;
        dynamic _tricksCategories;

        public CategoriesController() {
            _categoriesTable = new Categories();
            _tricksTable = new Tricks();
            _tricksCategories = new TricksCategories();
        }

        [Authorize(Roles = "Administrator")]
        public ActionResult Index()
        {
            //create a viewmodel so we can see the name of the trick
            //var x = _commentsTable.GetCategoriesForIndex();
            var x = _categoriesTable.All();
            return View(x);
        }

        [Authorize(Roles = "Administrator")]
        public ActionResult Create()
        {
            var data = _categoriesTable.Prototype;
            dynamic model = new ExpandoObject();
            model.CategoryData = data;

            List<dynamic> dropDownListOfTricksMulti = GetDropDownListOfTricksForMultiSelect();
            model.DropDownListOfTricksMulti = dropDownListOfTricksMulti;

            return View(model);
        } 

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public ActionResult Create(FormCollection collection)
        {
            var itemToCreate = _categoriesTable.CreateFrom(collection);

            try
            {
                _categoriesTable.Insert(itemToCreate);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["Error"] = "There was an error adding the trick: "+ ex.Message;
                //DateFromSQLToNZ(itemToCreate);
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
            var data = _categoriesTable.Get(ID: id);
            dynamic model = new ExpandoObject();
            model.CategoryData = data;

            List<dynamic> dropDownListOfTricksMulti = GetDropDownListOfTricksForMultiSelect(id);
            model.DropDownListOfTricksMulti = dropDownListOfTricksMulti;

            //List<dynamic> listOfTricksWhichAreSelectedInACategory = GetListOfTricksWhichAreSelectedInACategory(id);
            //model.ListOfTricksWhichAreSelectedInACategory = listOfTricksWhichAreSelectedInACategory;

            return View(model);
        }

        private List<dynamic> GetDropDownListOfTricksForMultiSelect(int categoryID = 0) {
            var dropDownListOfTricks = _tricksTable.Query("SELECT ID as TrickID, Name as TrickName FROM Tricks");
            //enumerate the list and populate
            List<dynamic> listOfx = new List<dynamic>();
            foreach (var item in dropDownListOfTricks) {
                dynamic x = new ExpandoObject();
                //find out if this should be selected
                //is it coming from created, so don't need to worry about anything being selected
                if (categoryID != 0) {
                    var tricksWhichAreSelected = _tricksCategories.Query("SELECT TrickID FROM TricksCategories WHERE CategoryID = " + categoryID + " AND TrickID = " + item.TrickID);
                    if (Enumerable.Count(tricksWhichAreSelected) == 1)
                        x.Selected = true;
                    else
                        x.Selected = false;
                }
                else
                    x.Selected = false;
                
                x.TrickID = item.TrickID;
                x.TrickName = item.TrickName;
                listOfx.Add(x);
            }
            return listOfx;
        }

        private List<dynamic> GetDropDownListOfTricks() {
            var dropDownListOfTricks = _tricksTable.Query("SELECT ID as TrickID, Name as TrickName FROM Tricks");
            //enumerate the list and populate
            List<dynamic> listOfx = new List<dynamic>();
            foreach (var item in dropDownListOfTricks) {
                dynamic x = new ExpandoObject();
                x.TrickID = item.TrickID;
                x.TrickName = item.TrickName;
                listOfx.Add(x);
            }
            return listOfx;
        }

        //private List<dynamic> GetListOfTricksWhichAreSelectedInACategory(int categoryID) {
        //    var tricksWhichAreSelected = _tricksCategories.Query("SELECT TrickID FROM TricksCategories WHERE CategoryID = " + categoryID);
        //    //enumerate the list and populate
        //    List<dynamic> listOfx = new List<dynamic>();
        //    foreach (var item in tricksWhichAreSelected) {
        //        dynamic x = new ExpandoObject();
        //        x.TrickID = item.TrickID;
        //        listOfx.Add(x);
        //    }
        //    return listOfx;
        //}

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public ActionResult Edit(int id, FormCollection collection)
        {
            string csvListOfTrickIDs = collection["TrickID"];
            string[] arrayOfTrickIDs = null;
            try {
                arrayOfTrickIDs = csvListOfTrickIDs.Split(',');
            }
            catch { }

            var itemToUpdate = _categoriesTable.CreateFrom(collection);

            try
            {
                _categoriesTable.Update(itemToUpdate, id);

                //if there are any categories selected
                if (arrayOfTrickIDs != null) {
                    //delete entries from TricksCategories
                    _tricksCategories.Delete(where: "CategoryID = " + id.ToString());

                    //insert entries into TrickCategories
                    foreach (var trickid in arrayOfTrickIDs) {
                        var trickCategoryToInsert = new {
                            TrickID = trickid,
                            CategoryID = id
                        };
                        _tricksCategories.Insert(trickCategoryToInsert);
                    }
                }


                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["Error"] = "There was an error editing this trick: " + ex.Message;
                return View(itemToUpdate);
            }
        }

        [Authorize(Roles = "Administrator")]
        public ActionResult Delete(int id)
        {
            var model = _categoriesTable.Get(ID: id);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                _categoriesTable.Delete(id);
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
