using PSI;
using PSI.UserAuthentication;
using System.Diagnostics;
using Xunit.Abstractions;
using Xunit.Sdk;


namespace XUnitTests
{
    public class RegistrationTest
    {
        private readonly ITestOutputHelper _testOutputHelper;
        public RegistrationTest(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
        }

        [Fact]
        public void TestIncorrectEmail()
        {
            string passwordOne = "password1";
            string passwordTwo = "password2";

            SignUpPage signUpPage = new()
            {
                Password = passwordOne,
                RepeatPassword = passwordTwo
            };



            signUpPage.OnSignUpClicked(this, new());

                
                _testOutputHelper.WriteLine(signUpPage.ErrorBody);

                string expected = "something";
                Assert.Equal(signUpPage.ErrorBody, expected);
            

        }

        [Fact]
        public void TestCorrectEmail()
        {
            string correctEmail = "email@gmail.com";
            Assert.True(correctEmail.IsEmailExtension() == true);
        }

    }
}