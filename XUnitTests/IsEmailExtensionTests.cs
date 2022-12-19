using PSI;
using System.Diagnostics;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace XUnitTests
{
    [Collection("Our Test Collection #2")]

    public class RegexTest
    {
        private readonly ITestOutputHelper _testOutputHelper;
        public RegexTest(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
        }

        [Fact]
        public void IsEmailExtension_NoAtSymbol_ReturnsFalse()
        {
            string mailStr = "emailgmail.com";
            Assert.True(mailStr.IsEmailExtension() == false);

        }

        [Fact]
        public void IsEmailExtension_NoTLD_ReturnsFalse()
        {
            string mailStr = "email@gmail";
            Assert.True(mailStr.IsEmailExtension() == false);

        }

        [Fact]
        public void IsEmailExtension_NoDomainName_ReturnsFalse()
        {
            string mailStr = "email@.com";
            Assert.True(mailStr.IsEmailExtension() == false);

        }

        [Fact]
        public void IsEmailExtension_NoDomain_ReturnsFalse()
        {
            string mailStr = "email@";
            Assert.True(mailStr.IsEmailExtension() == false);

        }

        [Fact]
        public void IsEmailExtension_NoUsername_ReturnsFalse()
        {
            string mailStr = "@gmail.com";
            Assert.True(mailStr.IsEmailExtension() == false);

        }

        [Fact]
        public void IsEmailExtension_NotAtStartOfString_ReturnsFalse()
        {
            string mailStr = " email@gmail.com";
            Assert.True(mailStr.IsEmailExtension() == false);

        }

        [Fact]
        public void IsEmailExtension_NotAtEndOfString_ReturnsFalse()
        {
            string mailStr = "email@gmail.com ";
            Assert.True(mailStr.IsEmailExtension() == false);

        }

        [Fact]
        public void IsEmailExtension_HasOnlyAt_ReturnsFalse()
        {
            string mailStr = "@";
            Assert.True(mailStr.IsEmailExtension() == false);

        }

        [Theory]
        [InlineData("email@gmail.com")]
        [InlineData("a@gmail.com")]
        [InlineData("email@g.com")]
        [InlineData("a@yahoo.lt")]
        public void IsEmailExtension_IsValid_ReturnsTrue(string correctEmail)
        {
            Assert.True(correctEmail.IsEmailExtension() == true);
        }

    }
}