namespace PSI.Views;

public partial class ReportView : ContentPage
{
	public ReportView()
	{
		InitializeComponent();
	}

	async void OnCancelButtonClicked(object sender, EventArgs e)
	{
        await Shell.Current.GoToAsync("..");
    }
}