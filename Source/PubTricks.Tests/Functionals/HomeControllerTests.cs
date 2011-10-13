using System.Web.Mvc;
using NUnit.Framework;
using PubTricks.Tests.Infrastructure;
using PubTricks.Web.Controllers;
using System.Linq;

namespace PubTricks.Tests.Functionals {
    [TestFixture]
    public class HomeControllerTests : TestBase {
        HomeController _controller;

        public HomeControllerTests() {
            this.Describes("Public Controllers Tests");
            _controller = new HomeController(new FakeLogger());
        }

        [Test]
        public void home_controller_should_return_a_result() {
            var result = _controller.Index() as ViewResult;
            Assert.IsNotNull(result);
        }

        //thanks
        [Test]
        public void thanks_page_should_return_a_result() {
            var result = _controller.Thanks() as ViewResult;
            Assert.IsNotNull(result);
        }

        //rendering tabs on front page
        [Test]
        public void a_user_should_be_able_to_view_10_newest_tricks_in_latest_videos_tab() {
            var result = _controller.Index() as ViewResult;
            dynamic viewModelExpando = result.ViewData.Model;
            var queryFromMassiveDynamic = viewModelExpando.TenTricksNewestFirst;

            var i = Enumerable.Count(queryFromMassiveDynamic);
            Assert.AreNotEqual(0, i, "TenTricksNewestFirst returned 0 records");
            Assert.LessOrEqual(i,10, "Ten Tricks Newest First returned more than 10 tricks");
        }

        [Test]
        public void a_user_should_be_able_to_view_10_most_popular_tricks_in_most_popular_tab() {
            var result = _controller.Index() as ViewResult;
            dynamic viewModelExpando = result.ViewData.Model;
            var queryFromMassiveDynamic = viewModelExpando.TenTricksMostPopularFirst;

            var i = Enumerable.Count(queryFromMassiveDynamic);

            Assert.AreNotEqual(0, i, "TenTricksMostPopularFirst returned 0 records");
            Assert.LessOrEqual(i, 10, "TenTricksMostPopularFirst returned more than 10 tricks");
        }

        //performance
        [Test]
        public void rendering_of_home_page_should_be_less_than_one_second_with_one_thousand_tricks_in_database() {
            this.IsPending();
        }
    }
}
