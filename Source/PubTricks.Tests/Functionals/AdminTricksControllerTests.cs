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
    [TestFixture]
    public class AdminTricksControllerTests : TestBase {

        public AdminTricksControllerTests() {
            this.Describes("Admin Controllers Tests");
            _tricksTable = new Tricks();
            _commentsTable = new Comments();
        }

        [SetUp]
        public void Init() {
            CleanTestDB();
            //_commentsTable.Delete();
            //_tricksTable.Delete();
        }

        //create trick - name
        [Test]
        public void new_trick_should_be_saved_without_error() {
            var controller = new TricksController();
            var formCollection = new FormCollection() {
                                                        { "Name", "asdf" },
                                                        { "Description", "test descr"},
                                                        { "Votes", "4" },
                                                        { "VideoURL", "" }
                                                      };
            var result = controller.Create(formCollection) as ViewResult;

            string errorMessage = "";
            if (result != null)
                errorMessage = result.TempData["Error"].ToString();
            Assert.IsEmpty(errorMessage);
        }

        [Test]
        public void a_new_trick_should_not_be_saved_when_empty_trick_name() {
            var controller = new TricksController();
            var formCollection = new FormCollection() {
                                                                    { "Name", "" },
                                                                    {"Description", "test descr"},
                                                                    { "Votes", "2" },
                                                                    { "VideoURL", "" }
                                                       };
            var result = controller.Create(formCollection) as ViewResult;
            var errorMessage = result.TempData["Error"];
            Assert.AreEqual("There was an error adding the trick: Can't insert: Name is required", errorMessage);
        }

        [Test]
        public void a_new_trick_should_not_be_saved_to_db_when_empty_trick_description() {
            var controller = new TricksController();
            var formCollection = new FormCollection() {
                                                                    { "Name", "test" },
                                                                    { "Description", "" },
                                                                    { "Votes", "2" },
                                                                    { "VideoURL", "" }
                                                                 };
            var result = controller.Create(formCollection) as ViewResult;
            var errorMessage = result.TempData["Error"];
            Assert.AreEqual("There was an error adding the trick: Can't insert: Description is required; Need more than 5 characters in the description", errorMessage);
        }

        [Test]
        public void a_new_trick_should_not_be_saved_to_db_when_an_existing_trick_in_the_db_has_the_duplicate_name() {
            var controller = new TricksController();
            var formCollection = new FormCollection() {
                                                                    { "Name", "test" },
                                                                    { "Description", "test desc" },
                                                                    { "Votes", "1" },
                                                                    { "VideoURL", "" }
                                                                 };
            var formCollection2 = new FormCollection() {
                                                                    { "Name", "test" },
                                                                    { "Description", "test desc2" },
                                                                    { "Votes", "2" },
                                                                    { "VideoURL", "" }
                                                                 };
            var result = controller.Create(formCollection) as ViewResult;
            var result2 = controller.Create(formCollection2) as ViewResult;

            var errorMessage = result2.TempData["Error"];
            Assert.AreEqual("There was an error adding the trick: Can't insert: Duplicate names of tricks not allowed", errorMessage);
        }

        [Test]
        public void a_new_trick_should_not_be_saved_to_db_when_an_existing_trick_in_the_db_has_duplicate_videourl() {
            var controller = new TricksController();
            var formCollection = new FormCollection() {
                                                                    { "Name", "test" },
                                                                    { "Description", "test desc" },
                                                                    { "Votes", "1" },
                                                                    { "VideoURL", "www.youtube.com/v/OQXZXat-RPQ?version=3" }
                                                                 };
            var formCollection2 = new FormCollection() {
                                                                    { "Name", "test2" },
                                                                    { "Description", "test desc2" },
                                                                    { "Votes", "2" },
                                                                    { "VideoURL", "www.youtube.com/v/OQXZXat-RPQ?version=3" }
                                                                 };
            var result = controller.Create(formCollection) as ViewResult;
            var result2 = controller.Create(formCollection2) as ViewResult;

            var errorMessage = result2.TempData["Error"];
            Assert.AreEqual("There was an error adding the trick: Can't insert: Duplicate VideoURL of tricks not allowed", errorMessage);
        }

        [Test]
        public void a_new_trick_should_not_be_saved_to_db_when_description_is_less_than_or_equal_to_five_chars_long() {
            var controller = new TricksController();
            var formCollection = new FormCollection() {
                                                                    { "Name", "test" },
                                                                    { "Description", "testd" },
                                                                    { "Votes", "1" },
                                                                    { "VideoURL", "" }
                                                                 };

            var result = controller.Create(formCollection) as ViewResult;

            var errorMessage = result.TempData["Error"];
            Assert.AreEqual("There was an error adding the trick: Can't insert: Need more than 5 characters in the description", errorMessage);
        }

        //create trick - votes
        [Test]
        public void a_new_trick_should_be_saved_to_db_when_empty_votes_and_should_default_to_zero_votes() {
            var controller = new TricksController();
            var formCollection = new FormCollection() {
                                                                    { "Name", "test" },
                                                                    { "Description", "test desc" },
                                                                    { "Votes", "" },
                                                                    { "VideoURL", "" }

                                                                 };
            var result = controller.Create(formCollection) as ViewResult;
            string errorMessage = "";
            if (result != null)
                errorMessage = result.TempData["Error"].ToString();
            Assert.IsEmpty(errorMessage);
        }

        [Test]
        public void a_new_trick_should_not_be_saved_to_db_on_add_trick_when_votes_are_a_non_number() {
            var controller = new TricksController();
            var formCollection = new FormCollection() {
                                                                    { "Name", "test" },
                                                                    {"Description", "test descr"},
                                                                    { "Votes", "x" },
                                                                    { "VideoURL", "" }
                                                                 };
            var result = controller.Create(formCollection) as ViewResult;
            string errorMessage = GetErrorMessageIfThereIsOne(result);
            Assert.AreEqual("There was an error adding the trick: Can't insert: Votes should be a number", errorMessage);
        }

        private static string GetErrorMessageIfThereIsOne(ViewResult result) {
            string errorMessage = "";
            if (result != null)
                errorMessage = result.TempData["Error"].ToString();
            return errorMessage;
        }

        //create trick - datecreated
        [Test]
        public void a_new_trick_should_be_saved_to_db_when_date_created_field_is_blank_and_set_to_now() {
            var controller = new TricksController();
            var formCollection = new FormCollection() {
                                                            { "Name", "test" },
                                                            { "Description", "test desc" },
                                                            { "Votes", "" },
                                                            { "VideoURL", "" },
                                                            { "DateCreated", ""}
                                                      };
            //on success result is always null
            var result = controller.Create(formCollection) as ViewResult;
            
            //can't find a way of passing back data easiy
            var modelJustSaved = _tricksTable.Get(Name: "test");

            DateTime dateCreatedAsDateTime = Convert.ToDateTime(modelJustSaved.DateCreated);
            var dateCreatedAsDateTimeShortFormat = dateCreatedAsDateTime.ToString("dd/MM/yyyy HH:mm");

            // 22/08/2011 23:05 once in a while this may fail.. refactor
            Assert.AreEqual(DateTime.Now.ToString("dd/MM/yyyy HH:mm"), dateCreatedAsDateTimeShortFormat);

            //on fail result holds the errors list
            string errorMessage = "";
            if (result != null)
                errorMessage = result.TempData["Error"].ToString();
            Assert.IsEmpty(errorMessage);
        }

        [Test]
        public void a_new_trick_should_be_saved_to_db_when_date_created_field_is_filled_in_nz_date_format() {
            var controller = new TricksController();
            var formCollection = new FormCollection() {
                                                            { "Name", "test" },
                                                            { "Description", "test desc" },
                                                            { "Votes", "" },
                                                            { "VideoURL", "" },
                                                            { "DateCreated", "15/10/2011"}
                                                      };
            //on success result is always null
            var result = controller.Create(formCollection) as ViewResult;

            //can't find a way of passing back data easiy
            var modelJustSaved = _tricksTable.Get(Name: "test");

            DateTime dateCreatedAsDateTime = Convert.ToDateTime(modelJustSaved.DateCreated);
            var dateCreatedAsDateTimeShortFormat = dateCreatedAsDateTime.ToString("dd/MM/yyyy");

            // 22/08/2011 23:05 once in a while this may fail.. refactor
            Assert.AreEqual("15/10/2011", dateCreatedAsDateTimeShortFormat);

            //on fail result holds the errors list
            string errorMessage = "";
            if (result != null)
                errorMessage = result.TempData["Error"].ToString();
            Assert.IsEmpty(errorMessage);
        }

        [Test]
        public void a_new_trick_should_not_be_saved_to_db_when_date_created_field_is_filled_in_us_date_format() {
            var controller = new TricksController();
            var formCollection = new FormCollection() {
                                                            { "Name", "test" },
                                                            { "Description", "test desc" },
                                                            { "Votes", "" },
                                                            { "VideoURL", "" },
                                                            { "DateCreated", "10/17/2011"}
                                                      };
            //on success result is always null
            var result = controller.Create(formCollection) as ViewResult;

            //on fail result holds the errors list
            string errorMessage = "";
            if (result != null)
                errorMessage = result.TempData["Error"].ToString();
            Assert.IsNotEmpty(errorMessage);
        }

        //edit trick - datecreated
        [Test]
        public void an_existing_trick_should_be_saved_to_db_when_everything_is_good_and_date_in_nz() {
            PopulateDBWithTestData();
            var controller = new TricksController();
            //get id of Pen Trick
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

        [Test]
        public void an_existing_trick_should_not_be_saved_to_db_when_date_created_field_is_filled_in_us_date_format() {
            PopulateDBWithTestData();
            var controller = new TricksController();
            //get id of Pen Trick
            var model = _tricksTable.Get(Name: "Pen Trick");
            int idOfPenTrick = model.ID;
            var formCollection = new FormCollection() {
                                                        { "ID", idOfPenTrick.ToString() },
                                                        { "Name", "Pen Trick" },
                                                        { "Description", "test descrxxx"},
                                                        { "Votes", "2" },
                                                        { "VideoURL", "" },
                                                        { "DateCreated", "10/17/2011"}
                                                      };
            var result = controller.Edit(idOfPenTrick, formCollection) as ViewResult;

            //on fail result holds the errors list
            string errorMessage = "";
            if (result != null)
                errorMessage = result.TempData["Error"].ToString();
            Assert.IsNotEmpty(errorMessage);
        }


        //edit trick name - in fact validation is mostly the same on edit except on duplicate name and videourl.
        [Test]
        public void an_existing_trick_should_be_saved_to_db_when_everything_is_good() {
            PopulateDBWithTestData();
            var controller = new TricksController();
            //get id of Pen Trick
            var model = _tricksTable.Get(Name: "Pen Trick");
            int idOfPenTrick = model.ID;
            var formCollection = new FormCollection() {
                                                        { "ID", idOfPenTrick.ToString() },
                                                        { "Name", "Pen Trick" },
                                                        { "Description", "test descrxxx"},
                                                        { "Votes", "2" },
                                                        { "VideoURL", "" }
                                                      };
            var result = controller.Edit(idOfPenTrick, formCollection) as ViewResult;
            string errorMessage = GetErrorMessageIfThereIsOne(result);
            Assert.IsEmpty(errorMessage);
        }

        [Test]
        public void an_existing_trick_should_not_be_saved_to_db_when_empty_trick_name_is_passed() {
            PopulateDBWithTestData();
            var controller = new TricksController();
            //get id of Pen Trick
            var model = _tricksTable.Get(Name: "Pen Trick");
            int idOfPenTrick = model.ID;
            var formCollection = new FormCollection() {
                                                        { "Name", "" },
                                                        { "Description", "test descr"},
                                                        { "Votes", "2" },
                                                        { "VideoURL", "" }
                                                      };
            var result = controller.Edit(idOfPenTrick, formCollection) as ViewResult;
            var errorMessage = result.TempData["Error"];
            Assert.AreEqual("There was an error editing this trick: Can't Update: Name is required", errorMessage);
        }

        [Test]
        public void an_existing_trick_should_not_be_saved_to_db_when_name_changed_to_an_existing_one_in_the_db() {
            //puts in Pen Trick, and Uncross Your Arms
            PopulateDBWithTestData();
            var controller = new TricksController();
            var model = _tricksTable.Get(Name: "Uncross Your Arms");
            int idOfUncrossYourArms = model.ID;

            var formCollection = new FormCollection() {
                                                        { "ID", idOfUncrossYourArms.ToString() },
                                                        //change Uncross Your Arms to Pen Trick - not allowed
                                                        { "Name", "Pen Trick" },
                                                        { "Description", "test descr"},
                                                        { "Votes", "2" },
                                                        { "VideoURL", "" }
                                                      };
            var result = controller.Edit(idOfUncrossYourArms, formCollection) as ViewResult;
            string errorMessage = GetErrorMessageIfThereIsOne(result);
            Assert.AreEqual("There was an error editing this trick: Can't Update: Duplicate names of tricks not allowed", errorMessage);
        }

        [Test]
        public void an_existing_trick_should_not_be_saved_to_db_when_videourl_changed_to_an_existing_one_in_the_db() {
            //puts in Pen Trick, and Uncross Your Arms
            PopulateDBWithTestData();
            var controller = new TricksController();
            var model = _tricksTable.Get(Name: "Uncross Your Arms");
            int idOfUncrossYourArms = model.ID;

            string videoURLOfPenTrick = @"www.youtube.com/v/OQXZXat-RPQ?version=3";

            var formCollection = new FormCollection() {
                                                        { "ID", idOfUncrossYourArms.ToString() },
                                                        //change Uncross Your Arms to Pen Trick - not allowed
                                                        { "Name", "Uncross Your Arms" },
                                                        { "Description", "test descr"},
                                                        { "Votes", "2" },
                                                        { "VideoURL", videoURLOfPenTrick }
                                                      };
            var result = controller.Edit(idOfUncrossYourArms, formCollection) as ViewResult;
            string errorMessage = GetErrorMessageIfThereIsOne(result);
            Assert.AreEqual("There was an error editing this trick: Can't Update: Duplicate VideoURL of tricks not allowed", errorMessage);
        }


        //trick admin authentication and authorisation
        [Test]
        public void all_trick_admin_screens_and_posts_require_logged_in_and_member_of_administrators_role() {
            //this.IsPending();
        }

        [Test]
        public void all_trick_admin_posts_require_cross_site_scripting_turned_on() {
            // this.IsPending();
        }
        
    }
}
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
