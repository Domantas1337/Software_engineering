namespace PSI.UserAuthentication;

public partial class SignInPage : ContentPage
{
    public String Password { set; get; }

    public SignInPage()
    {
        InitializeComponent();
    }

    private async void TapGestureRecognizer_Tapped_For_SignUP(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(SignUpPage));
    }

    public void EmailEntered(object sender, EventArgs e)
    {
        string email = ((Entry)sender).Text;

        if (email.isEmail())
        {
            signInNotice.Text = "";
        }
        else
        {
            signInNotice.Text = "Invalid Email";
        }
    }

}