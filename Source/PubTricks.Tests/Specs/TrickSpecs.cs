using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using PubTricks.Web.Models;
using Massive;

namespace PubTricks.Tests.Specs {
    [TestFixture]
    public class TrickSpecs : TestBase {
        DynamicModel _tbl;

        public TrickSpecs() {
            this.Describes("PubTricks admin");
            _tbl = new DynamicModel("PubTricks", "Tricks", "ID");
        }

        [SetUp]
        public void Init() {
            _tbl.Delete();
        }

        [Test]
        public void a_new_trick_should_be_saved_to_db_on_add_trick() {
            Trick trick = new Trick();
            string name = "test name";
            var result = trick.AddTrick(name, "test desc", "test url");
            Assert.AreEqual(1, _tbl.All().Count());
        }

        [Test]
        public void a_new_trick_should_be_saved_to_db_on_add_trick_and_correct_result_returned() {
            Trick trick = new Trick();
            string name = "test name b";
            var result = trick.AddTrick(name, "test desc", "test url");
            var trickFromDB = _tbl.All(where: "WHERE Name=@0", args: name);
            Assert.AreEqual(1, trickFromDB.Count());
        }

        [Test]
        public void duplicate_name_of_new_trick_should_fail_to_save() {
            Trick trick = new Trick();
            var result = trick.AddTrick("test name", "test desc", "test url");
            var result2 = trick.AddTrick("test name", "test desc2", "test url2");
            Assert.IsFalse(result2.Success, "Result success should be false");
            Assert.AreEqual(result2.Message, "Duplicate names of tricks not allowed");
        }

        [Test]
        public void duplicate_videourl_of_new_trick_should_fail_to_save() {
            Trick trick = new Trick();
            var result = trick.AddTrick("test name", "test desc", "test url");
            var result2 = trick.AddTrick("test name2", "test desc2", "test url");
            Assert.IsFalse(result2.Success, "Result success should be false");
            Assert.AreEqual(result2.Message, "Duplicate VideoURL of tricks not allowed");
        }

        [Test]
        public void description_should_be_more_than_5_chars_long() {
            Trick trick = new Trick();
            string name = "test name";
            var result = trick.AddTrick(name, "test", "test url");
            Assert.IsFalse(result.Success, "Result success should be false");
            Assert.AreEqual(result.Message, "Need more than 5 characters in the description");
        }
    }
}
