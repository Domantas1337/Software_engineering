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
using PSI.Views.ManageLocation;
using PSI.Verification;

namespace PSI.Views;

public partial class MainView : ContentPage
{
    protected Lazy<Task<ObservableCollection<LocationItem>>> locations;
    private readonly ILocationService _locationService;
    private readonly ILogService _logService;

    public MainView(ReportViewModel vm, ILocationService locationService, ILogService logService)
    {
        if (DeviceInfo.Platform != DevicePlatform.Unknown)
        {
            InitializeComponent();
            BindingContext = vm;
        }

        _logService = logService;
        _locationService = locationService;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        locations = new Lazy<Task<ObservableCollection<LocationItem>>>(() =>
        Task.Run(
         async () => {
             List<LocationItem> locationList = await _locationService.GetAllLocationItemsAsync();
            //List<LocationItem> locationList = await _locationService.GetAllLocationItemsAsync();
            return locationList.ToObservableCollection();
            })
        );

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
    public async void MapButtonClicked(object sender, EventArgs e) => await Shell.Current.GoToAsync(nameof(LocationsView), new Dictionary<string, object>
                                                                {
                                                                    {
                                                                        "Locations", locations.Value.Result
                                                                    }
                                                                });
    async void OnAddItemClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(AddLocationView));
    }
        async void OnAuthenticationClicked(object sender, EventArgs e) => await Shell.Current.GoToAsync(nameof(SignInPage));
    async void OnReportButtonClicked(object sender, EventArgs e) => await Shell.Current.GoToAsync(nameof(ReportView));
    


}
