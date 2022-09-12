namespace PSI;

public partial class SelectPage : ContentPage
{
	public SelectPage()
	{
		InitializeComponent();
    }

	// Pakeistiiiiiiiiiiiiiiiiiii
	private void somethingClicked(object sender, EventArgs e)
	{
        SelectionState.utilityState = UtilityState.Something;
        App.Current.MainPage = new NavigationPage(new MainPage());
    }
    private void taromatClicked(object sender, EventArgs e)
    {
        SelectionState.utilityState = UtilityState.Taromat;
        App.Current.MainPage = new NavigationPage(new MainPage());
    }
    private void trashCanClicked(object sender, EventArgs e)
    {
        SelectionState.utilityState = UtilityState.TrashCan;
        App.Current.MainPage = new NavigationPage(new MainPage());
    }
}