namespace PSI;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();

        AlertManager alertManager= new AlertManager();
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
