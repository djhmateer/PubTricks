using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using PubTricks.Web.Models;
using Massive3;

namespace PubTricks.Tests.Specs {
    [TestFixture]
    public class TrickSpecs : TestBase {
        DynamicModel _tbl;
        Tricks _trick;

        public TrickSpecs() {
            this.Describes("PubTricks admin");
            _trick = new Tricks();
            _tbl = new DynamicModel("PubTricks", "Tricks", "ID");
        }

        [SetUp]
        public void Init() {
            _tbl.Delete();
        }

        //admin authentication and authorisation
        [Test]
        public void all_admin_screens_and_posts_require_logged_in_and_member_of_administrators_role() {
            this.IsPending();
        }

        [Test]
        public void all_admin_posts_cross_site_scripting_turned_on() {
            this.IsPending();
        }

        //create, read, update, delete tricks - testing business logic and persistence
        [Test]
        public void a_new_trick_should_be_saved_to_db_on_add_trick() {
            string nameOfTrick = "test name";
            var result = _trick.AddTrick(nameOfTrick, "test desc", "test url");

            Assert.AreEqual(1, _tbl.All().Count());
        }

        [Test]
        public void a_new_trick_should_be_saved_to_db_on_add_trick_and_correct_result_returned() {
            string name = "test name b";
            var result = _trick.AddTrick(name, "test desc", "test url");
            var trickFromDB = _tbl.All(where: "WHERE Name=@0", args: name);
            Assert.AreEqual(1, trickFromDB.Count());
        }

        [Test]
        public void duplicate_name_of_new_trick_should_fail_to_save() {
            var result = _trick.AddTrick("test name", "test desc", "test url");
            var result2 = _trick.AddTrick("test name", "test desc2", "test url2");
            Assert.IsFalse(result2.Success, "Result success should be false");
            Assert.AreEqual(result2.Message, "Duplicate names of tricks not allowed");
        }

        [Test]
        public void duplicate_videourl_of_new_trick_should_fail_to_save() {
            var result = _trick.AddTrick("test name", "test desc", "test url");
            var result2 = _trick.AddTrick("test name2", "test desc2", "test url");
            Assert.IsFalse(result2.Success, "Result success should be false");
            Assert.AreEqual(result2.Message, "Duplicate VideoURL of tricks not allowed");
        }

        [Test]
        public void description_should_be_more_than_5_chars_long() {
            string name = "test name";
            var result = _trick.AddTrick(name, "test", "test url");
            Assert.IsFalse(result.Success, "Result success should be false");
            Assert.AreEqual(result.Message, "Need more than 5 characters in the description");
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

        //google seo
        [Test]
        public void site_should_generate_a_file_for_seo_on_every_admin_create_edit_update_del() {
            this.IsPending();
        }

        //performance
        [Test]
        public void rendering_of_all_tricks_page_should_be_less_than_one_second_with_two_hundred_tricks() {
            this.IsPending();
        }
    }
}
