using System.Threading.Tasks;

namespace PSI.Views;

public partial class SelectionView : ContentPage
{
    public SelectionView()
    {
        InitializeComponent();
    }

    // Pakeistiiiiiiiiiiiiiiiiiii
    async private void somethingClicked(object sender, EventArgs e)
    {
        PageState.utilityState = UtilityState.Something;
        await Shell.Current.GoToAsync("..");
    }
    async private void taromatClicked(object sender, EventArgs e)
    {
        PageState.utilityState = UtilityState.Taromat;
        await Shell.Current.GoToAsync("..");
    }
    async private void trashCanClicked(object sender, EventArgs e)
    {
        PageState.utilityState = UtilityState.TrashCan;
        await Shell.Current.GoToAsync("..");
    }
}

