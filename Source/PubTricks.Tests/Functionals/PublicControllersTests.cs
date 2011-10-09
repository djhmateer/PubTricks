using System.Web.Mvc;
using NUnit.Framework;
using PubTricks.Tests.Infrastructure;
using PubTricks.Web.Controllers;

namespace PubTricks.Tests.Functionals {
    [TestFixture]
    public class PublicControllersTests : TestBase {
        public PublicControllersTests() {
            this.Describes("Public Controllers Tests");
        }

        [Test]
        public void home_controller_should_return_a_result() {
            HomeController controller = new HomeController(new FakeLogger());
            ViewResult result = controller.Index() as ViewResult;
            //Assert.AreEqual("Welcome to ASP.NET MVC!", result.ViewBag.Message);
            Assert.IsNotNull(result);
        }

        [Test]
        public void tricks_controller_should_return_a_result() {
            TricksController controller = new TricksController();
            ViewResult result = controller.Index() as ViewResult;
            Assert.IsNotNull(result);
        }

    }
}
