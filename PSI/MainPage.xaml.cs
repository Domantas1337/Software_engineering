namespace PSI;

public partial class MainPage : ContentPage
{
	private int count = 0;

    public MainPage()
    {
        InitializeComponent();
        Label label = new Label { Text = SelectionState.utilityState.ToString() };

        mainLayout.Children.Add(label);
    }

    private void SelectButtonClicked(object sender, EventArgs e)
    {
        App.Current.MainPage = new NavigationPage(new SelectPage());
    }
}
