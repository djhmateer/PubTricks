using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace PubTricks.Tests.Specs {
    [TestFixture]
    public class CategorySpecs : TestBase {
        [Test]
        public void an_admin_should_be_able_to_add_a_new_category() {
            this.IsPending();
        }

        [Test]
        public void an_admin_should_be_able_to_edit_an_existing_category() {
            this.IsPending();
        }

        [Test]
        public void a_duplicate_category_name_should_fail_to_save() {
            this.IsPending();
        }

        [Test]
        public void a_category_name_of_less_then_3_chars_should_fail_to_save() {
            this.IsPending();
        }

        [Test]
        public void an_admin_should_be_able_to_delete_an_existing_category_only_when_there_are_no_tricks_associated_with_it() {
            this.IsPending();
        }
    }
}
