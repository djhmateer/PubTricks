using System;
using System.Diagnostics;
using NUnit.Framework;
using PubTricks.Web.Models;
using System.Dynamic;

namespace PubTricks.Tests {
    public class TestBase {
        protected dynamic _tricksTable;
        protected dynamic _commentsTable;
        protected dynamic _categoriesTable;
        protected dynamic _tricksCategoriesTable;

        public TestBase() {
            _tricksTable = new Tricks();
            _commentsTable = new Comments();
            _categoriesTable = new Categories();
            _tricksCategoriesTable = new TricksCategories();
        }

        public void Describes(string description) {
            Console.WriteLine("-------------------------------");
            Console.WriteLine(description);
            Console.WriteLine("-------------------------------");
        }

        public void IsPending() {
            Console.WriteLine(GetCaller() + " -- PENDING --");
            Assert.Inconclusive();
        }

        public string GetCaller() {
            StackTrace stack = new StackTrace();
            return stack.GetFrame(2).GetMethod().Name.Replace("_", " ");
        }

        protected void CleanTestDB() {
            _commentsTable.Delete();
            _tricksCategoriesTable.Delete();
            _categoriesTable.Delete();
            _tricksTable.Delete();
        }

        protected dynamic PopulateDBWithTestData() {
            dynamic returnThing = new ExpandoObject();
            var result1 = _tricksTable.Insert(new {
                Name = "Pen Trick",
                Description = "This is the pen trick description",
                VideoURL = @"www.youtube.com/v/OQXZXat-RPQ?version=3",
                Votes = "11",
                Thumbnail = @"PenTrickImage-100x100.png",
                LongDescription = @"The pen trick is one of my favourite all time tricks.. if there is one you remember try this one!",
                VideoSolutionURL = @"www.youtube.com/v/ILEWo_-Fib8?version=3"
            });
            var idOfPenTrickAsString = Convert.ToString(result1.ID);
            var idOfPenTrick = Convert.ToInt32(result1.ID);

            returnThing.IDOfPenTrick = idOfPenTrick;

            var result2 = _tricksTable.Insert(new {
                Name = "Uncross Your Arms",
                Description = "Uncross your arms description - very funny to watch",
                VideoURL = @"www.youtube.com/v/2_3BJq5srL4?version=3",
                Votes = "7",
                Thumbnail = @"UncrossArms-100x100.png",
                LongDescription = @"This is a good trick especially for kids or friends who have drunk more than 4 pints of beer (or 2 pints of cider.. never again...)",
                VideosolutionURL = @"www.youtube.com/v/IyctbzxAA7U?version=3"
            });

            _tricksTable.Insert(new {
                Name = "Test Trick",
                Description = "Uncross your arms description - very funny to watch",
                VideoURL = @"www.youtube.com/v/2_3BJq5srL4?version=3x",
                Votes = "7",
                Thumbnail = @"UncrossArms-100x100.png",
                LongDescription = @"This is a good trick especially for kids or friends who have drunk more than 4 pints of beer (or 2 pints of cider.. never again...)",
                VideosolutionURL = @"www.youtube.com/v/IyctbzxAA7U?version=3x"
            });


            //put in a few comments for Pen Trick
            var result3 = _commentsTable.Insert(new {
                TrickID = idOfPenTrickAsString,
                CommentText = "This is a comment1"
            });

            var result4 = _commentsTable.Insert(new {
                TrickID = idOfPenTrickAsString,
                CommentText = "This is a comment2"
            });

            //put in a few categories for Pen Trick
            result1 = _categoriesTable.Insert(new { 
                Name = "Kids"
            });

            result2 = _categoriesTable.Insert(new {
                Name = "Funny"
            });

            returnThing.IDOfFunnyCategory = Convert.ToInt32(result2.ID);

            result3 = _categoriesTable.Insert(new {
                Name = "Physical"
            });

            _tricksCategoriesTable.Insert(new {
                TrickID = idOfPenTrickAsString,
                CategoryID = result1.ID
            });

            _tricksCategoriesTable.Insert(new {
                TrickID = idOfPenTrickAsString,
                CategoryID = result2.ID
            });

            _tricksCategoriesTable.Insert(new {
                TrickID = idOfPenTrickAsString,
                CategoryID = result3.ID
            });

            return returnThing;
        }
    }
}
