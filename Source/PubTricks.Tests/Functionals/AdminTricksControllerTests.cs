using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using PubTricks.Web.Areas.Admin.Controllers;
using System.Web.Mvc;
using Massive;
using PubTricks.Web.Models;

namespace PubTricks.Tests.Functionals {
    public class AdminTricksControllerTests : TestBase {
        DynamicModel _tbl;
        Tricks _trick;

        public AdminTricksControllerTests() {
            this.Describes("Admin Controllers Tests");
            _trick = new Tricks();
            _tbl = new DynamicModel("PubTricks", "Tricks", "ID");
        }

        [SetUp]
        public void Init() {
            _tbl.Delete();
        }

        //crud on the admintrickscontroller - testing validation
        //validation of tricks
        [Test]
        public void a_new_trick_should_be_saved_to_db_on_add_trick_and_correct_result_returned() {
            this.IsPending();
        }

        [Test]
        public void a_new_trick_should_not_be_saved_to_db_when_empty_trick_name() {
            TricksController controller = new TricksController();
            //ViewResult result = controller.Index() as ViewResult;
            // var formCollection = new FormCollection();


            FormCollection formCollection = new FormCollection() {
                                                                    { "Name", "" }
                                                                 };
            //rootController.ValueProvider = formValues.ToValueProvider();

            ViewResult result = controller.Create(formCollection) as ViewResult;
            var errorMessage = result.TempData["Error"];
            //Can't insert: Name is required

            Assert.AreEqual("There was an error adding the trick: Can't insert: Name is required", errorMessage);
            Assert.IsNotNull(result);
            

            //string name = "";
            //var result = _trick.AddTrick(name, "test desc", "test url");
            //if (!result.Success)
            //    DisplaySQLErrorInConsoleWindow(result);
            //var trickFromDB = _tbl.All(where: "WHERE Name=@0", args: name);
            //Assert.AreEqual(1, trickFromDB.Count());
        }



        //[Test]
        //public void home_controller_should_return_a_result() {
        //    HomeController controller = new HomeController(new FakeLogger());
        //    ViewResult result = controller.Index() as ViewResult;
        //    //Assert.AreEqual("Welcome to ASP.NET MVC!", result.ViewBag.Message);
        //    Assert.IsNotNull(result);
        //}

        //[Test]
        //public void tricks_controller_should_return_a_result() {
        //    TricksController controller = new TricksController();
        //    ViewResult result = controller.Index() as ViewResult;
        //    //Assert.AreEqual("Welcome to ASP.NET MVC!", result.ViewBag.Message);
        //    Assert.IsNotNull(result);
        //}

        //trick admin authentication and authorisation
        [Test]
        public void all_trick_admin_screens_and_posts_require_logged_in_and_member_of_administrators_role() {
            this.IsPending();
        }

        [Test]
        public void all_trick_admin_posts_require_cross_site_scripting_turned_on() {
            this.IsPending();
        }

        //trick crud on admin
        [Test]
        public void create_trick_inserts_trick_into_db() {

            this.IsPending();
        }
    }
}
