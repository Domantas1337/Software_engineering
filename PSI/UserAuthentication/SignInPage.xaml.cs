using PSI.FileManagers;
using PSI.Models;
using System.Security.Cryptography;

namespace PSI.UserAuthentication;

public partial class SignInPage : ContentPage
{
    public string Password { set; get; }
    public string Email { get; set; }

    public SignInPage()
    {
        InitializeComponent();
        BindingContext = this;
    }

    private async void TapGestureRecognizer_Tapped_For_SignUP(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(SignUpPage));
    }

    public async void OnSignInClicked(object sender, EventArgs e)
    {
        if (!Email.IsEmailExtension())
        {
            signInNotice.Text = "Invalid Email";
        }
        else
        {
            List<UserDataItem> usersData = JSONFileManager<UserDataItem>.Read(Constants.UsersFilePath);
            SHA512 shaManaged = new SHA512Managed();
            byte[] result = shaManaged.ComputeHash(Convert.FromBase64String(Password));
            var newData = (from item in usersData
                          where item.PasswordHash == result
                          && item.Email == Email
                          select item.Name).ToList();
            if (newData.Count == 1)
            {
                signInNotice.Text = newData.First();
            }
            await Shell.Current.GoToAsync("..");
        }
    }
}