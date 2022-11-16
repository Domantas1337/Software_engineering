using Microsoft.Maui.Controls;
using PSI.Models;
using PSI.Services;
using PSI.UserAuthentication;
using PSI.ViewModels;
using System.Data;

namespace PSI.Views;

public partial class MainView : ContentPage
{

    private readonly IRestService _dataService;
    public List<LocationItem> locations; 
    public MainView(ReportViewModel vm, IRestService dataService)
    {
        InitializeComponent();
        BindingContext = vm;

        _dataService = dataService;
    }

    protected async override void OnAppearing()
    {
        base.OnAppearing();

        locations = await _dataService.GetAllToDosAsync();
        for(int i = 0; i < locations.Count; i++)
        {
            Debug.WriteLine(locations[i].Street);
        }
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
    async void MapButtonClicked(object sender, EventArgs e) => await Shell.Current.GoToAsync(nameof(LocationsView), new Dictionary<string, object>
                                                                {
                                                                    {
                                                                        "Locations", locations
                                                                    }
                                                                });
    async void StateButtonClicked(object sender, EventArgs e) => await Shell.Current.GoToAsync(nameof(SelectionView));
    async void OnAddItemClicked(object sender, EventArgs e) => await Shell.Current.GoToAsync(nameof(AddLocationView));
    async void OnAuthenticationClicked(object sender, EventArgs e) => await Shell.Current.GoToAsync(nameof(SignInPage));
    async void OnReportButtonClicked(object sender, EventArgs e) => await Shell.Current.GoToAsync(nameof(ReportView));
}