using System.Web.Mvc;
using NUnit.Framework;
using PubTricks.Tests.Infrastructure;
using PubTricks.Web.Controllers;

namespace PubTricks.Tests.Functionals {
    [TestFixture]
    public class HomeControllerTests : TestBase {
        public HomeControllerTests() {
            this.Describes("Home Controller Tests");
        }

        [Test]
        public void home_controller_should_return_welcome_message() {
            HomeController controller = new HomeController(new FakeLogger());
            ViewResult result = controller.Index() as ViewResult;
            Assert.AreEqual("Welcome to ASP.NET MVC!", result.ViewBag.Message);
        }

        [Test]
        public void about_controller_should_return_a_viewresult() {
            HomeController controller = new HomeController(new FakeLogger());
            ViewResult result = controller.About() as ViewResult;
            Assert.IsNotNull(result);
        }
    }
}
