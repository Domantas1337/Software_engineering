using PSI;
using System.Diagnostics;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace XUnitTests
{
    public class RegexTest
    {
        private readonly ITestOutputHelper _testOutputHelper;
        public RegexTest(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
        }

        [Fact]
        public void TestIncorrectEmail()
        {
            string noEnd = "email@gmail.";
            string noAt = "emailgmail.com";
            Assert.True(noEnd.IsEmailExtension() == false);
            Assert.True(noAt.IsEmailExtension() == false);

        }

        [Fact]
        public void TestCorrectEmail()
        {
            string correctEmail = "email@gmail.com";
            Assert.True(correctEmail.IsEmailExtension() == true);
        }

    }
}