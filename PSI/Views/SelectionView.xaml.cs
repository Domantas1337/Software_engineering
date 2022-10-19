using PSI.State;
using System.Threading.Tasks;

namespace PSI.Views;

public partial class SelectionView : ContentPage
{
    public SelectionView()
    {
        InitializeComponent();
    }

    async private void SomethingClicked(object sender, EventArgs e)
    {
        AppState.utilityState = UtilityState.Litter;
        await Shell.Current.GoToAsync("..");
    }
    async private void TaromatClicked(object sender, EventArgs e)
    {
        AppState.utilityState = UtilityState.Taromat;
        await Shell.Current.GoToAsync("..");
    }
    async private void TrashCanClicked(object sender, EventArgs e)
    {
        AppState.utilityState = UtilityState.TrashCan;
        await Shell.Current.GoToAsync("..");
    }
}

