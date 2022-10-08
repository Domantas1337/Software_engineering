using PSI.UserAuthentication;

namespace PSI.Views;

public partial class MainView : ContentPage
{
	public MainView()
	{
		InitializeComponent();
	}

    async void StateButtonClicked(object senderm, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(SelectionView));
    }
    async void OnAddItemClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(AddLocationView));
    }

    async void OnAuthenticationClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(SignInPage));
    }
    async void OnReportButtonClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(ReportView));
    }
    async void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(AddLocationView));
    }
}