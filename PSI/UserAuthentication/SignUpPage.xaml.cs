using Microsoft.Maui.ApplicationModel.Communication;
using System.Reflection.Metadata.Ecma335;
using System.Security.Cryptography;
using PSI.Models;
using PSI.FileManagers;

namespace PSI.UserAuthentication;

public partial class SignUpPage : ContentPage
{
    public string Username { get; set; }
    public string Password { get; set; }
    public string RepeatPassword { get; set; }
    public string Email { get; set; }

    public string ErrorBody { get; set; } = "Invalid:";

    public SignUpPage()
    {
        InitializeComponent();
        BindingContext = this;
    }

    private async void TapGestureRecognizer_Tapped_For_SignIn(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("..");
    }

    public async void OnSignUpClicked(object sender, EventArgs e)
    {
        bool errored = false;
        if (!Email.IsEmailExtension())
        {
            ErrorBody += " email ";
            errored = true;
        }
        if (Password != RepeatPassword)
        {
            ErrorBody += " password ";
            errored = true;
        }
        if (errored)
        {
            signUpNotice.Text = ErrorBody;
            ErrorBody = "Invalid:";
        }
        else
        {
            SHA512 shaManaged = new SHA512Managed();
            byte[] result = shaManaged.ComputeHash(Convert.FromBase64String(Password));
            UserDataItem userData = new()
            {
                Name = this.Username,
                Email = this.Email,
                PasswordHash = result
            };

            JSONFileManager<UserDataItem>.Write(
                                        item: userData,
                                        filePath: Constants.UsersFilePath
                                        );

            await Shell.Current.GoToAsync("..");
        }
    }
}