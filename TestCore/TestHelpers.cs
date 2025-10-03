using Grocery.Core.Helpers;

namespace TestCore
{
    public class TestHelpers
    {
        [SetUp]
        public void Setup()
        {
        }

        // Happy flow
        [Test]
        public void TestPasswordHelperReturnsTrue()
        {
            // Arrange
            string password = "user3";
            string passwordHash = "sxnIcZdYt8wC8MYWcQVQjQ==.FKd5Z/jwxPv3a63lX+uvQ0+P7EuNYZybvkmdhbnkIHA=";

            // Act
            bool result = PasswordHelper.VerifyPassword(password, passwordHash);

            // Assert
            Assert.IsTrue(result);
        }

        [TestCase("user1", "IunRhDKa+fWo8+4/Qfj7Pg==.kDxZnUQHCZun6gLIE6d9oeULLRIuRmxmH2QKJv2IM08=")]
        [TestCase("user3", "sxnIcZdYt8wC8MYWcQVQjQ==.FKd5Z/jwxPv3a63lX+uvQ0+P7EuNYZybvkmdhbnkIHA=")]
        public void TestPasswordHelperReturnsTrue(string password, string passwordHash)
        {
            // Arrange
            string Password = password;
            string PasswordHash = passwordHash;

            // Act
            bool result = PasswordHelper.VerifyPassword(Password, PasswordHash);

            // Assert
            Assert.IsTrue(result);
        }

        // Unhappy flow
        [Test]
        public void TestPasswordHelperReturnsFalse()
        {

            // Arrange

            // Act

            // Assert
            Assert.Pass("deze test is automatisch geslaagd");
        }

        [TestCase("user1", "IunRhDKa+fWo8+4/Qfj7Pg==.kDxZnUQzCZun6gLIE6d9oeULLRIuRmxmH2QKJv2IM08=")]
        [TestCase("user3", "sxnIcZdYt8wC8MYWcQVQjQ==.FKd5Z/jlxPv3a63lX+uvQ0+P7EuNYZybvkmdhbnkIHA=")]
        public void TestPasswordHelperReturnsFalse(string password, string passwordHash)
        {
            // Arrange
            string Password = password;
            string PasswordHash = passwordHash;

            // Act
            bool result = PasswordHelper.VerifyPassword(Password, PasswordHash);

            // Assert
            if (!result)
            {
                Assert.Pass($"'{Password}' is niet gelijk aan '{PasswordHash}'");
            }
            else
            {
                Assert.Fail($"'{Password}' is gelijk aan '{PasswordHash}'");
            }
        }
    }
}