using PSI.Models;
using PSI.Services;
using PSI.UserAuthentication;
using PSI.ViewModels;


namespace PSI.Views;

public partial class MainView : ContentPage
{
    ITodoService _todoService;
    public MainView(ReportViewModel vm, ITodoService todoService)
    {
        InitializeComponent();
        BindingContext = vm;

        _todoService = todoService;

    }

    public async void GenerateReportPage(object sender, SelectedItemChangedEventArgs args)
    {
        ReportItem item = (ReportItem)args.SelectedItem;

        
        await Shell.Current.GoToAsync(nameof(ReportDetailPage), new Dictionary<string, object>
                                                                {
                                                                    { 
                                                                        "AReport", item.Report
                                                                    }
                                                                }
        );
    }

    async void StateButtonClicked(object senderm, EventArgs e) => await Shell.Current.GoToAsync(nameof(SelectionView));
    async void OnAddItemClicked(object sender, EventArgs e) => await Shell.Current.GoToAsync(nameof(AddLocationView));
    async void OnAuthenticationClicked(object sender, EventArgs e) => await Shell.Current.GoToAsync(nameof(SignInPage));
    async void OnReportButtonClicked(object sender, EventArgs e) => await Shell.Current.GoToAsync(nameof(ReportView));
}