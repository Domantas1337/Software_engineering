namespace PSI.Views;

public partial class MainView : ContentPage
{
	public MainView()
	{
		InitializeComponent();
	}

    async void OnAddItemClicked(object sender, EventArgs e)
    {

        await Shell.Current.GoToAsync(nameof(AddLocationView));
    }

    async void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(AddLocationView));
    }
}