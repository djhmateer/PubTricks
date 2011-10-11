using System.Web.Mvc;
using NUnit.Framework;
using PubTricks.Tests.Infrastructure;
using PubTricks.Web.Controllers;

namespace PubTricks.Tests.Functionals {
    [TestFixture]
    public class TricksControllerTests : TestBase {
        public TricksControllerTests() {
            this.Describes("Tricks Controller Tests");
        }

        [Test]
        public void tricks_controller_should_return_a_result() {
            //TricksController controller = new TricksController();
            //ViewResult result = controller.Index() as ViewResult;
            //Assert.IsNotNull(result);
            this.IsPending();
        }

        //details page friendly urls
        [Test]
        public void when_a_user_types_in_url_with_friendly_trick_name_and_minuses_for_spaces_it_all_works() {
            this.IsPending();
        }

        [Test]
        public void when_a_user_types_in_url_with_friendly_trick_name_and_get_case_wrong_it_works() {
            this.IsPending();
        }

        [Test]
        public void when_a_user_types_in_url_with_friendly_trick_name_and_gets_it_wrong_redirect_to_error_page() {
            this.IsPending();
        }


        //likes
        [Test]
        public void when_a_user_clicks_on_like_button_on_a_trick_increment_votes() {
            this.IsPending();
        }

        [Test]
        public void a_user_should_be_able_to_like_a_video_only_once() {
            this.IsPending();
        }

        [Test]
        public void when_a_user_clicks_on_like_button_on_a_trick_and_hasnt_done_so_set_a_cookie_on_their_browser() {
            this.IsPending();

        }

        [Test]
        public void when_a_user_clicks_on_like_button_on_a_trick_and_hasnt_done_so_set_a_cookie_on_their_browser_to_expire_in_1_year() {
            this.IsPending();
        }

        [Test]
        public void when_trick_page_renders_check_to_see_if_user_has_a_cookie_set_and_hide_the_like_button_if_they_do_and_display_liked() {
            this.IsPending();
        }

        //details page sharing
        [Test]
        public void when_a_user_clicks_on_share_trick_on_facebook_they_are_redirected_to_correct_page() {
            this.IsPending();
        }

        [Test]
        public void when_a_user_clicks_on_tweet_this_trick_they_are_redirected_to_correct_page() {
            this.IsPending();
        }

        //all details pages
        [Test]
        public void all_details_pages_render_without_errors() {
            this.IsPending();
        }

        //performance
        [Test]
        public void rendering_of_home_page_should_be_less_than_one_second_with_one_thousand_tricks_in_database() {
            this.IsPending();
        }

    }
}
