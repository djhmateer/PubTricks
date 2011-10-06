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

        [Test]
        public void a_user_should_be_able_to_like_a_video() {
            this.IsPending();
        }

        [Test]
        public void a_user_should_be_able_to_like_a_video_only_once() {
            this.IsPending();
        }

        [Test]
        public void an_admin_should_be_able_to_add_a_trick() {
            this.IsPending();
        }

        [Test]
        public void an_admin_should_be_able_to_add_a_category_to_a_trick() {
            this.IsPending();
        }

        [Test]
        public void an_admin_should_be_able_to_add_2_categories_to_a_trick() {
            this.IsPending();
        }

        [Test]
        public void an_admin_should_be_able_to_delete_a_category_from_a_trick() {
            this.IsPending();
        }

        [Test]
        public void an_admin_should_be_able_to_delete_all_categories_from_a_trick() {
            this.IsPending();
        }

        [Test]
        public void a_new_trick_should_be_saved_to_db_on_add_trick() {
            Tricks trick = new Tricks();
            string name = "test name";
            var result = trick.AddTrick(name, "test desc", "test url");
            Assert.AreEqual(1, _tbl.All().Count());
        }

        [Test]
        public void a_new_trick_should_be_saved_to_db_on_add_trick_and_correct_result_returned() {
            Tricks trick = new Tricks();
            string name = "test name b";
            var result = trick.AddTrick(name, "test desc", "test url");
            var trickFromDB = _tbl.All(where: "WHERE Name=@0", args: name);
            Assert.AreEqual(1, trickFromDB.Count());
        }

        [Test]
        public void duplicate_name_of_new_trick_should_fail_to_save() {
            Tricks trick = new Tricks();
            var result = trick.AddTrick("test name", "test desc", "test url");
            var result2 = trick.AddTrick("test name", "test desc2", "test url2");
            Assert.IsFalse(result2.Success, "Result success should be false");
            Assert.AreEqual(result2.Message, "Duplicate names of tricks not allowed");
        }

        [Test]
        public void duplicate_videourl_of_new_trick_should_fail_to_save() {
            Tricks trick = new Tricks();
            var result = trick.AddTrick("test name", "test desc", "test url");
            var result2 = trick.AddTrick("test name2", "test desc2", "test url");
            Assert.IsFalse(result2.Success, "Result success should be false");
            Assert.AreEqual(result2.Message, "Duplicate VideoURL of tricks not allowed");
        }

        [Test]
        public void description_should_be_more_than_5_chars_long() {
            Tricks trick = new Tricks();
            string name = "test name";
            var result = trick.AddTrick(name, "test", "test url");
            Assert.IsFalse(result.Success, "Result success should be false");
            Assert.AreEqual(result.Message, "Need more than 5 characters in the description");
        }
    }
}
