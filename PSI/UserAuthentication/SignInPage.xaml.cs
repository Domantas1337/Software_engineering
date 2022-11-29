using PSI.FileManagers;
using PSI.Models;
using System.Security.Cryptography;
using System.Text;

namespace PSI.UserAuthentication;

public partial class SignInPage : ContentPage
{

    public string Name { get; set; }
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
            List<UserDataItem> usersData = await JSONManager<UserDataItem>.ReadAsync(Constants.UsersFilePath);
            List<String> newData = (from item in usersData
                           where item.Email.Equals(Email)
                           && item.Password.Equals(Password)
                           select item.Name).ToList();
            if (newData.Count > 0)
            {
                signedInNotice.Text = newData.First();
                signInNotice.Text = String.Empty;
            }
            else
            {
                signInNotice.Text = "Invalid signin";
                signedInNotice.Text = String.Empty;
            }
        }
    }
}