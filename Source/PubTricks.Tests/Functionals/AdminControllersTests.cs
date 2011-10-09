using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using PubTricks.Web.Controllers;
using System.Web.Mvc;

namespace PubTricks.Tests.Functionals {
    public class AdminControllersTests : TestBase {
        public AdminControllersTests() {
            this.Describes("Admin Controllers Tests");
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
    }
}
