using NUnit.Framework;

namespace PubTricks.Tests {
    [TestFixture]
    public class MembershipSpecs : TestBase {
        public MembershipSpecs() {
            this.Describes("User Registration");
        }

        [Test]
        // acceptance test or gateway test
        public void valid_email_and_passwords_should_register_user() {
            Assert.AreEqual(1, 2);
        }
    }
}
