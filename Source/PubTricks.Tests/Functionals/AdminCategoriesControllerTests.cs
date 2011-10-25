using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using PubTricks.Web.Models;
using PubTricks.Web.Areas.Admin.Controllers;
using System.Web.Mvc;
using System.Diagnostics;

namespace PubTricks.Tests.Functionals {
    [TestFixture]
    public class AdminCategoriesControllerTests : TestBase {

        public AdminCategoriesControllerTests() {
            this.Describes("Admin Comments Tests");
            //_tricksTable = new Tricks();
            //_commentsTable = new Comments();
            //_categoriesTable = new Categories();
            //_tricksCategoriesTable = new TricksCategories();
        }

        [SetUp]
        public void Init() {
            CleanTestDB();
        }

        //admin category index
        [Test]
        public void admin_category_index_page_should_render() {
            PopulateDBWithTestData();
            var controller = new CategoriesController();
            var result = controller.Index() as ViewResult;
            dynamic x = result.ViewData.Model;

            //var dropDownListOfTricks = x.DropDownListOfTricks;
            var countOfAllCategories = Enumerable.Count(x);

            Assert.AreEqual(3, countOfAllCategories);
        }

        [Test]
        public void admin_category_create_page_should_render() {
            PopulateDBWithTestData();
            var controller = new CategoriesController();
            var result = controller.Create() as ViewResult;
            dynamic x = result.ViewData.Model;
            Assert.IsNotNull(x);
        }

        [Test]
        public void admin_category_create_page_should_render_and_display_a_list_of_all_tricks() {
            PopulateDBWithTestData();
            var controller = new CategoriesController();
            var result = controller.Create() as ViewResult;
            dynamic x = result.ViewData.Model;

            var dropDownListOfTricks = x.DropDownListOfTricks;
            var countOfAllTricks = Enumerable.Count(dropDownListOfTricks);

            Assert.AreEqual(3, countOfAllTricks);
        }

        //create category
        [Test]
        public void new_category_should_be_saved_without_error() {
            var idOfPenTrick = PopulateDBWithTestData();
            var controller = new CategoriesController();
            var formCollection = new FormCollection() {
                                                        { "Name", "Test Category" }
                                                      };
            var result = controller.Create(formCollection) as ViewResult;

            string errorMessage = "";
            if (result != null)
                errorMessage = result.TempData["Error"].ToString();
            Assert.IsEmpty(errorMessage);
        }

        //edit category
        [Test]
        public void edit_category_should_render() {
            var idOfFunnyCategory = PopulateDBWithTestData().IDOfFunnyCategory;

            var controller = new CategoriesController();
            var result = controller.Edit(idOfFunnyCategory) as ViewResult;
            dynamic x = result.ViewData.Model;
            Assert.IsNotNull(x);
            //var dropDownListOfTricks = x.DropDownListOfTricks;
            //var count = Enumerable.Count(dropDownListOfTricks);
            //Assert.AreEqual(2, count);
        }

        //edit category render with multiselect
        [Test]
        public void edit_category_page_should_render_with_multiple_tricks_selected_in_dropdown() {
            var idOfFunnyCategory = PopulateDBWithTestData().IDOfFunnyCategory;
            var controller = new CategoriesController();
            var result = controller.Edit(idOfFunnyCategory) as ViewResult;

            dynamic x = result.ViewData.Model;

            var dropDownListOfTricks = x.DropDownListOfTricks;
            var count = Enumerable.Count(dropDownListOfTricks);
            Assert.AreEqual(3, count);
        }

        [Test]
        public void edit_category_page_should_save_with_multiple_tricks_selected_in_dropdown() {
            int idOfFunnyCategory = PopulateDBWithTestData().IDOfFunnyCategory;
            var controller = new CategoriesController();
            var model = _tricksTable.Get(Name: "Pen Trick");
            int idOfPenTrick = model.ID;
            var formCollection = new FormCollection() {
                                                        { "ID", idOfPenTrick.ToString() },
                                                        { "Name", "Pen Trick" },
                                                        { "Description", "test descrxxx"},
                                                        { "Votes", "2" },
                                                        { "VideoURL", "" },
                                                        { "DateCreated", "15/10/2011"}
                                                      };
            var result = controller.Edit(idOfPenTrick, formCollection) as ViewResult;
            string errorMessage = GetErrorMessageIfThereIsOne(result);
            Assert.IsEmpty(errorMessage);

            var modelJustSaved = _tricksTable.Get(Name: "Pen Trick");
            DateTime dateCreatedAsDateTime = Convert.ToDateTime(modelJustSaved.DateCreated);
            var dateCreatedAsDateTimeShortFormat = dateCreatedAsDateTime.ToString("dd/MM/yyyy HH:mm");

            Assert.AreEqual("15/10/2011 00:00", dateCreatedAsDateTimeShortFormat);
        }

        private static string GetErrorMessageIfThereIsOne(ViewResult result) {
            string errorMessage = "";
            if (result != null)
                errorMessage = result.TempData["Error"].ToString();
            return errorMessage;
        }

        //del category
        [Test]
        public void del_category_should_render() {
            var idOfFunnyCategory = PopulateDBWithTestData().IDOfFunnyCategory;

            var controller = new CategoriesController();
            var result = controller.Delete(idOfFunnyCategory) as ViewResult;
            dynamic x = result.ViewData.Model;
            Assert.IsNotNull(x);
            //var dropDownListOfTricks = x.DropDownListOfTricks;
            //var count = Enumerable.Count(dropDownListOfTricks);
            //Assert.AreEqual(2, count);
        }

       
    }
}
