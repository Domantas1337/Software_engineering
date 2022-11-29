using PSI.ViewModels;

namespace PSI.Views;

public partial class ReportView : ContentPage
{
    public ReportView(ReportViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }

    async void OnCancelButtonClicked(object sender, EventArgs e) => await Shell.Current.GoToAsync("..");

}