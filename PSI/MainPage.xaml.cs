namespace PSI;

public partial class MainPage : ContentPage
{
	private int count = 0;

    public MainPage()
    {
        InitializeComponent();
        changeStateLabel();
    }

    public void changeStateLabel()
    {
        stateLabel.Text = SelectionState.utilityState.ToString();
    }

    private void SelectButtonClicked(object sender, EventArgs e)
    {
        App.Current.MainPage = new NavigationPage(new SelectPage());
    }
}
