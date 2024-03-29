using Microsoft.Maui.ApplicationModel.Communication;
using System.Reflection.Metadata.Ecma335;
using System.Security.Cryptography;
using PSI.Models;
using PSI.FileManagers;
using System.Text;
using PSI.Generators;

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
        if (DeviceInfo.Platform != DevicePlatform.Unknown)
        { 
            InitializeComponent();
            BindingContext = this;
        }
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
            ErrorBody += " passwords don't match ";
            errored = true;
        }
        if (errored)
        {
            if (DeviceInfo.Platform != DevicePlatform.Unknown)
            {
                signUpNotice.Text = ErrorBody;
                ErrorBody = "Invalid:";
            }
        }
        else
        {
            UserDataItem userData = new()
            {
                Id = IDGenerator.GenerateID(),
                Name = this.Username,
                Email = this.Email,
                Password = this.Password
            };


            await JSONManager.WriteAsync<UserDataItem>(
                                        item: userData,
                                        filePath: Constants.UsersFilePath
                                        );
            
            await Shell.Current.GoToAsync("..");
        }
    }
}