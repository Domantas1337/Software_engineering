using Moq;
using PSI.UserAuthentication;
using PSIAPI.Controllers;
using PSIAPI.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit.Abstractions;

namespace XUnitTests
{
    public class SignUpTests
    {
        private readonly ITestOutputHelper _testOutputHelper;
        public SignUpTests(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
        }

        [Fact]
        public void SignUp_PasswordsDontMatch_SecondPassword()
        {
            SignUpPage signUpPage= new();
            signUpPage.Email = "an.email@gmail.com";
            signUpPage.Password = "password";
            signUpPage.Username = "username";
            signUpPage.RepeatPassword= "passwordd";

            signUpPage.OnSignUpClicked(this, new EventArgs());

            _testOutputHelper.WriteLine(signUpPage.ErrorBody);


            Assert.True(signUpPage.ErrorBody == "Invalid: passwords don't match ");

        }

        [Fact]
        public void SignUp_IncorrcrEmail()
        {
            SignUpPage signUpPage = new();
            signUpPage.Email = "an.emailgmail.com";
            signUpPage.Password = "password";
            signUpPage.Username = "username";
            signUpPage.RepeatPassword = "password";

            signUpPage.OnSignUpClicked(this, new EventArgs());

            _testOutputHelper.WriteLine(signUpPage.ErrorBody);

            Assert.True(signUpPage.ErrorBody == "Invalid: email ");

        }

        [Fact]
        public void SignUp_IncorrcrEmailAndPasswordsDontMatch()
        {
            SignUpPage signUpPage = new();
            signUpPage.Email = "an.emailgmail.com";
            signUpPage.Password = "password";
            signUpPage.Username = "username";
            signUpPage.RepeatPassword = "passwordd";

            signUpPage.OnSignUpClicked(this, new EventArgs());

            _testOutputHelper.WriteLine(signUpPage.ErrorBody);

            Assert.True(signUpPage.ErrorBody == "Invalid: email  passwords don't match ");

        }


    }
}
