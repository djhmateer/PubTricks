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
        dynamic _tricksTable;

        public AdminTricksControllerTests() {
            this.Describes("Admin Controllers Tests");
            _tricksTable = new Tricks();
        }

        [SetUp]
        public void Init() {
            _tricksTable.Delete();
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
            VideoSolutionURL= @"www.youtube.com/v/ILEWo_-Fib8?version=3"
            });
        }
            //var result = _tricksTable.AddTrick(name: "Uncross Your Arms", description: "Uncross your arms description - very funny to watch",
            //    videourl: @"www.youtube.com/v/2_3BJq5srL4?version=3", votes: 7, thumbnail: @"UncrossArms-100x100.png",
            //    longdescription: @"This is a good trick especially for kids or friends who have drunk more than 4 pints of beer (or 2 pints of cider.. never again...)",
            //    videosolutionurl: @"www.youtube.com/v/IyctbzxAA7U?version=3");

            //result = _tricksTable.AddTrick(name: "Pen Trick", description: "This is the pen trick description",
            //    videourl: @"www.youtube.com/v/OQXZXat-RPQ?version=3", votes: 11, thumbnail: @"PenTrickImage-100x100.png",
            //    longdescription: @"The pen trick is one of my favourite all time tricks.. if there is one you remember try this one!",
            //    videosolutionurl: @"www.youtube.com/v/ILEWo_-Fib8?version=3");

            //result = _tricksTable.AddTrick(name: "Beer trap", description: "Beer trap description",
            //    videourl: @"www.youtube.com/v/NsXyrPN-eNo?version=3", votes: 6, thumbnail: @"Beer-100x100.png",
            //    longdescription: @"Best to do this one after your mates have had a lot to drink!");

            //result = _tricksTable.AddTrick(name: "Foot and Hand Circles", description: "Foot and Hand circles description",
            //    videourl: @"www.youtube.com/v/TsGhmK8Zgtc?version=3", votes: 3, thumbnail: @"FootAndHand-100x100.png",
            //    longdescription: @"Foot and hand circles requires some serious concentration!");

            //result = _tricksTable.AddTrick(name: "Coin Trick", description: "The amazing coin trick",
            //    videourl: @"www.youtube.com/v/-hnnpzBSnU8?version=3", votes: 8, thumbnail: @"CoinTrick-100x100.png",
            //    longdescription: @"The coin trick is a good one!",
            //    videosolutionurl: @"www.youtube.com/v/rlhAj5_i56I?version=3");

        //create trick name
        [Test]
        public void new_trick_should_be_saved_on_and_correct_result_returned() {
            TricksController controller = new TricksController();
            FormCollection formCollection = new FormCollection() {
                                                                    { "Name", "test" },
                                                                    {"Description", "test descr"},
                                                                    { "Votes", "4" }
                                                                 };
            ViewResult result = controller.Create(formCollection) as ViewResult;
            string errorMessage = "";
            try {
                errorMessage = result.TempData["Error"].ToString();
            }
            catch { }
            Assert.IsNullOrEmpty(errorMessage);
        }

        [Test]
        public void a_new_trick_should_not_be_saved_when_empty_trick_name() {
            TricksController controller = new TricksController();
            FormCollection formCollection = new FormCollection() {
                                                                    { "Name", "" },
                                                                    {"Description", "test descr"},
                                                                    { "Votes", "2" }
                                                                 };
            ViewResult result = controller.Create(formCollection) as ViewResult;
            var errorMessage = result.TempData["Error"];
            Assert.AreEqual("There was an error adding the trick: Can't insert: Name is required", errorMessage);
        }

        [Test]
        public void a_new_trick_should_not_be_saved_to_db_when_empty_trick_description() {
            TricksController controller = new TricksController();
            FormCollection formCollection = new FormCollection() {
                                                                    { "Name", "test" },
                                                                    { "Description", "" },
                                                                    { "Votes", "2" }
                                                                 };
            ViewResult result = controller.Create(formCollection) as ViewResult;
            var errorMessage = result.TempData["Error"];
            Assert.AreEqual("There was an error adding the trick: Can't insert: Description is required; Need more than 5 characters in the description", errorMessage);
        }

        [Test]
        public void a_new_trick_should_not_be_saved_to_db_when_an_existing_trick_in_the_db_has_the_duplicate_name() {
            TricksController controller = new TricksController();
            FormCollection formCollection = new FormCollection() {
                                                                    { "Name", "test" },
                                                                    { "Description", "test desc" },
                                                                    { "Votes", "1" }
                                                                 };
            FormCollection formCollection2 = new FormCollection() {
                                                                    { "Name", "test" },
                                                                    { "Description", "test desc2" },
                                                                    { "Votes", "2" }
                                                                 };
            ViewResult result = controller.Create(formCollection) as ViewResult;
            ViewResult result2 = controller.Create(formCollection2) as ViewResult;

            var errorMessage = result2.TempData["Error"];
            Assert.AreEqual("There was an error adding the trick: Can't insert: Duplicate names of tricks not allowed", errorMessage);
        }


        [Test]
        public void a_new_trick_should_not_be_saved_to_db_when_an_existing_trick_in_the_db_has_duplicate_videourl() {
            TricksController controller = new TricksController();
            FormCollection formCollection = new FormCollection() {
                                                                    { "Name", "test" },
                                                                    { "Description", "test desc" },
                                                                    { "Votes", "1" },
                                                                    { "VideoURL", "www.youtube.com/v/OQXZXat-RPQ?version=3" }
                                                                 };
            FormCollection formCollection2 = new FormCollection() {
                                                                    { "Name", "test2" },
                                                                    { "Description", "test desc2" },
                                                                    { "Votes", "2" },
                                                                    { "VideoURL", "www.youtube.com/v/OQXZXat-RPQ?version=3" }
                                                                 };
            ViewResult result = controller.Create(formCollection) as ViewResult;
            ViewResult result2 = controller.Create(formCollection2) as ViewResult;

            var errorMessage = result2.TempData["Error"]; 
            Assert.AreEqual("There was an error adding the trick: Can't insert: Duplicate VideoURL of tricks not allowed", errorMessage);
        }

        [Test]
        public void a_new_trick_should_not_be_saved_to_db_when_description_is_less_than_or_equal_to_five_chars_long() {
            TricksController controller = new TricksController();
            FormCollection formCollection = new FormCollection() {
                                                                    { "Name", "test" },
                                                                    { "Description", "testd" },
                                                                    { "Votes", "1" }
                                                                 };
           
            ViewResult result = controller.Create(formCollection) as ViewResult;

            var errorMessage = result.TempData["Error"];
            Assert.AreEqual("There was an error adding the trick: Can't insert: Need more than 5 characters in the description", errorMessage);
        }

        //create trick votes
        [Test]
        public void a_new_trick_should_be_saved_to_db_when_empty_votes_and_should_default_to_zero_votes() {
            TricksController controller = new TricksController();
            FormCollection formCollection = new FormCollection() {
                                                                    { "Name", "test" },
                                                                    { "Description", "test desc" },
                                                                    { "Votes", "" }
                                                                 };
            ViewResult result = controller.Create(formCollection) as ViewResult;
            //if no error then tempdate error wont exist and will give a nullreference exception
            string errorMessage="";
            try {
                errorMessage = result.TempData["Error"].ToString();
            }
            catch { }
            Assert.IsNullOrEmpty(errorMessage);
        }

        [Test]
        public void a_new_trick_should_not_be_saved_to_db_on_add_trick_when_votes_are_a_non_number() {
            TricksController controller = new TricksController();
            FormCollection formCollection = new FormCollection() {
                                                                    { "Name", "test" },
                                                                    {"Description", "test descr"},
                                                                    { "Votes", "x" }
                                                                 };
            ViewResult result = controller.Create(formCollection) as ViewResult;
            string errorMessage = "";
            try {
                errorMessage = result.TempData["Error"].ToString();
            }
            catch { }
            Assert.AreEqual("There was an error adding the trick: Can't insert: Votes should be a number", errorMessage);
        }

        //edit trick name
        [Test]
        public void an_existing_trick_should_not_be_saved_to_db_when_empty_trick_name_is_passed() {
            PopulateDBWithTestData();
            TricksController controller = new TricksController();
            //get id of Pen Trick
            var model = _tricksTable.Get(Name: "Pen Trick");
            int idOfPenTrick = model.ID;
            FormCollection formCollection = new FormCollection() {
                                                                    { "Name", "" },
                                                                     {"Description", "test descr"},
                                                                     { "Votes", "2" }
                                                                 };
            ViewResult result = controller.Edit(idOfPenTrick, formCollection) as ViewResult;
            var errorMessage = result.TempData["Error"];
            Assert.AreEqual("There was an error editing this trick: Can't Update: Name is required", errorMessage);
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
            //this.IsPending();
        }

        [Test]
        public void all_trick_admin_posts_require_cross_site_scripting_turned_on() {
            // this.IsPending();
        }

        //trick crud on admin
        [Test]
        public void create_trick_inserts_trick_into_db() {

            //this.IsPending();
        }
    }
}
