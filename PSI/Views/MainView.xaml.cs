using PSI.Models;
using PSI.UserAuthentication;
using PSI.ViewModels;

namespace PSI.Views;

public partial class MainView : ContentPage
{
    public MainView(ReportViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
    /*
        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            ReportItem report = (ReportItem)query["pav"];

            ReportTitle.Text = report.Title;
        }
    */

    async void StateButtonClicked(object senderm, EventArgs e) => await Shell.Current.GoToAsync(nameof(SelectionView));
    async void OnAddItemClicked(object sender, EventArgs e) => await Shell.Current.GoToAsync(nameof(AddLocationView));
    async void OnAuthenticationClicked(object sender, EventArgs e) => await Shell.Current.GoToAsync(nameof(SignInPage));
    async void OnReportButtonClicked(object sender, EventArgs e) => await Shell.Current.GoToAsync(nameof(ReportView));


}