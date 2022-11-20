using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.Maui.Controls;
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
    private readonly IRestService _dataService;
    public MainView(ReportViewModel vm, IRestService dataService)
    {
        InitializeComponent();
        BindingContext = vm;

        _dataService = dataService;
    }

    protected async override void OnAppearing()
    {
        base.OnAppearing();


        Debug.WriteLine("bbbb");

        List<LocationItem> temp = await _dataService.GetAllLocationItemsAsync();

        Debug.WriteLine("prasideda");
        Debug.WriteLine(temp.Count);

        locations = new ObservableCollection<LocationItem>();
        foreach (LocationItem item in temp)
        {
            locations.Add(
                new LocationItem()
                {
                    Street = item.Street,
                    City = item.City,
                    Id = item.Id,
                    Longitude = item.Longitude,
                    Latitude = item.Latitude,
                    State = item.State,
                    Position = new Location((double)item.Latitude, (double)item.Longitude)
                }
            );
        }

        locations.Add(
                new LocationItem()
                {
                    Street = "Street",
                    City = "City",
                    Id = 21,
                    Longitude = 1,
                    Latitude = 2,
                    Position = new Location(36.9628066, -122.0194722)
                }
            );


        Debug.WriteLine("Baigiasi");

        for (int i = 0; i < locations.Count; ++i)
        {
            Debug.WriteLine(locations[i].Street + locations[i].Position);
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
