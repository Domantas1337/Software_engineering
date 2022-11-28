using CommunityToolkit.Maui.Core.Extensions;
using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Maps;
using PSI.Models;
using PSI.Services;
using PSI.UserAuthentication;
using PSI.ViewModels;
using System.Collections.ObjectModel;
using System.Data;

namespace PSI.Views;

public partial class MainView : ContentPage
{

    public ObservableCollection<LocationItem> locations;
    public Location currentLocation = new(54.72908271722996, 25.264220631657665);
    private readonly IRestService _dataService;
    private readonly LogRestService _logRestService;

    private AddLocationView _addLocationView;
    public MainView(ReportViewModel vm, AddLocationView addLocationView, IRestService dataService, LogRestService logRestService)
    {
        InitializeComponent();
        BindingContext = vm;

        dataService.LocationsExist += OnLocationExists;

        _logRestService = logRestService;
        _dataService = dataService;
    }

    protected async override void OnAppearing()
    {
        base.OnAppearing();

        double distance = 1e9;
        LocationItem nearestLocation = default;

        List<LocationItem> locationList = await _dataService.GetAllLocationItemsAsync();
        locations = locationList.ToObservableCollection();
        
        foreach (LocationItem item in locations)
        {
            distance = currentLocation.CalculateDistance(item.Position, DistanceUnits.Kilometers) < distance ?
                       currentLocation.CalculateDistance(item.Position, DistanceUnits.Kilometers) : distance;
            nearestLocation = item;

            Debug.WriteLine(item.Street + " aaaa");
        }

        int y = 0;

        try
        {
            int x = 2 / y;
        }catch(Exception ex)
        {
            _logRestService.AddLocationItemAsync(new LogItem() { dateTime = new DateTime().ToString(), Id = new Guid().ToString("N"), exceptionDetails = ex.Message });
        }

    }

    public void OnLocationExists(object sender, LocationEventArgs e)
    {

        Debug.WriteLine("Answer: " + e.Message);

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
