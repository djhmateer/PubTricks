using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using PubTricks.Web.Models;
using PubTricks.Web.Areas.Admin.Controllers;
using System.Web.Mvc;
using System.Diagnostics;

namespace PubTricks.Tests.Functionals {
    [TestFixture]
    public class AdminCommentsControllerTests : TestBase {

        public AdminCommentsControllerTests() {
            this.Describes("Admin Comments Tests");
            _tricksTable = new Tricks();
            _commentsTable = new Comments();
        }

        [SetUp]
        public void Init() {
            CleanTestDB();
        }

        //create comment
        [Test]
        public void new_comment_should_be_saved_without_error() {
            var idOfPenTrick = PopulateDBWithTestData().IDOfPenTrick;
            var controller = new CommentsController();
            var formCollection = new FormCollection() {
                                                        { "TrickID", idOfPenTrick.ToString() },
                                                        { "CommentText", "This is a comment"}
                                                      };
            var result = controller.Create(formCollection) as ViewResult;

            string errorMessage = "";
            if (result != null)
                errorMessage = result.TempData["Error"].ToString();
            Assert.IsEmpty(errorMessage);
        }

        [Test]
        public void multiple_new_comments_should_be_saved_without_error() {
            var idOfPenTrick = PopulateDBWithTestData().IDOfPenTrick;
            var controller = new CommentsController();
            for (int i = 0; i < 10; i++) {
                var formCollection = new FormCollection() {
                                                        { "TrickID", idOfPenTrick.ToString() },
                                                        { "CommentText", "This is a comment: " + i.ToString()}
                                                      };
                var result = controller.Create(formCollection) as ViewResult;

                string errorMessage = "";
                if (result != null)
                    errorMessage = result.TempData["Error"].ToString();
                Assert.IsEmpty(errorMessage);
            }
        }

        //edit comment dropdownlist check
        [Test]
        public void edit_comment_page_should_render_with_a_drop_down_list_of_tricks() {
            var idOfPenTrick = PopulateDBWithTestData().IDOfPenTrick;
            var controller = new CommentsController();
            var result = controller.Edit(idOfPenTrick) as ViewResult;
            dynamic x = result.ViewData.Model;

            var dropDownListOfTricks = x.DropDownListOfTricks;
            var count = Enumerable.Count(dropDownListOfTricks);
            Assert.AreEqual(3, count);
        }
       
    }
}
