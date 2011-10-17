﻿using System.Web.Mvc;
using NUnit.Framework;
using PubTricks.Tests.Infrastructure;
using PubTricks.Web.Controllers;
using PubTricks.Web.Models;
using Massive3;
using System.Diagnostics;
using System.Linq;

namespace PubTricks.Tests.Functionals {
    [TestFixture]
    public class DetailTricksControllerTests : TestBase {
        dynamic _tricksTable;

        public DetailTricksControllerTests() {
            _tricksTable = new Tricks();
            this.Describes("Tricks Controller Tests");
        }

        private static void DisplaySQLErrorInConsoleWindow(dynamic result) {
            string errorMessage = result.Message;
            Trace.WriteLine("Error: " + errorMessage);
            throw new AssertionException("DB Error");
        }

        [SetUp]
        public void Init() {
            _tricksTable.Delete();

            PopulateDBWithTestData();

            //if (!result.Success)
            //    DisplaySQLErrorInConsoleWindow(result);
            //else
            //    Assert.AreEqual(5, _tbl.All().Count());
        }

        private void PopulateDBWithTestData() {
            //load database with test data going straight to massive
            //validation in tricks will be called when doing insert
            var result = _tricksTable.Insert(new {
                Name = "Pen Trick",
                Description = "This is the pen trick description",
                VideoURL = @"www.youtube.com/v/OQXZXat-RPQ?version=3",
                Votes = "11",
                Thumbnail = @"PenTrickImage-100x100.png",
                LongDescription = @"The pen trick is one of my favourite all time tricks.. if there is one you remember try this one!",
                VideoSolutionURL = @"www.youtube.com/v/ILEWo_-Fib8?version=3"
            });

            result = _tricksTable.Insert(new {
                Name = "Uncross Your Arms",
                Description = "Uncross your arms description - very funny to watch",
                VideoURL = @"www.youtube.com/v/2_3BJq5srL4?version=3",
                Votes = "7",
                Thumbnail = @"UncrossArms-100x100.png",
                LongDescription = @"This is a good trick especially for kids or friends who have drunk more than 4 pints of beer (or 2 pints of cider.. never again...)",
                VideosolutionURL = @"www.youtube.com/v/IyctbzxAA7U?version=3"
            });
        }

        [Test]
        public void tricks_controller_should_return_correct_data() {
            string trickNameToGet = "Pen Trick";
            var controller = new TricksController();
            ViewResult result = controller.Details(trickName: trickNameToGet) as ViewResult;

            dynamic viewModelExpando = result.ViewData.Model;
            string nameFromMassiveDynamicQuery = viewModelExpando.Name;

            Assert.AreEqual(trickNameToGet, nameFromMassiveDynamicQuery);
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
            var model = _tricksTable.Get(Name: "Pen Trick");
            int idOfPenTrick = model.ID;
            int numberOfLikesInitially = model.Votes;

            var controller = new TricksController();
            var formCollection = new FormCollection() {
                                                        { "Name", "asdf" }
                                                      };
            var result = controller.Details(formCollection, id: idOfPenTrick) as ViewResult;

            string errorMessage = "";
            if (result != null)
                errorMessage = result.TempData["Error"].ToString();
            Assert.IsEmpty(errorMessage);

            model = _tricksTable.Get(Name: "Pen Trick");
            int numberOfLikesFinally = model.Votes;

            Assert.AreEqual(numberOfLikesInitially + 1, numberOfLikesFinally);
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
