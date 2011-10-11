using System.Web.Mvc;
using NUnit.Framework;
using PubTricks.Tests.Infrastructure;
using PubTricks.Web.Controllers;

namespace PubTricks.Tests.Functionals {
    [TestFixture]
    public class HomeControllerTests : TestBase {
        public HomeControllerTests() {
            this.Describes("Public Controllers Tests");
        }

        [Test]
        public void home_controller_should_return_a_result() {
            HomeController controller = new HomeController(new FakeLogger());
            ViewResult result = controller.Index() as ViewResult;
            //Assert.AreEqual("Welcome to ASP.NET MVC!", result.ViewBag.Message);
            Assert.IsNotNull(result);
        }

        //[Test]
        //public void tricks_controller_should_return_a_result() {
        //    TricksController controller = new TricksController();
        //    ViewResult result = controller.Index() as ViewResult;
        //    Assert.IsNotNull(result);
        //}

        //thanks
        [Test]
        public void thanks_page_should_render() {
            this.IsPending();
        }

        //rendering on front page
        [Test]
        public void a_user_should_be_able_to_view_5_most_popular_tricks_in_video_slider() {
            this.IsPending();
        }

        [Test]
        public void a_user_should_be_able_to_view_10_most_recently_added_tricks_in_latest_videos_tab() {
            this.IsPending();
        }

        [Test]
        public void a_user_should_be_able_to_view_10_most_popular_tricks_in_most_popular_tab() {
            this.IsPending();
        }


        //performance
        [Test]
        public void rendering_of_home_page_should_be_less_than_one_second_with_one_thousand_tricks_in_database() {
            this.IsPending();
        }

    }
}
