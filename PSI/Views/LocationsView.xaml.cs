using PSI.Models;
using System.Collections.ObjectModel;

namespace PSI.Views;

[QueryProperty(nameof(Locations), "Locations")]
public partial class LocationsView : ContentPage
{
    public ObservableCollection<LocationItem> locations;
    private CancellationTokenSource _cancelTokenSource;
    private bool _isCheckingLocation;
    private Location location;


    public ObservableCollection<LocationItem> Locations
    {
        get => locations;
        set
        {
            locations = value;
            OnPropertyChanged();
        }
    }

    public async Task GetCurrentLocation()
    {
        try
        {
            _isCheckingLocation = true;

            GeolocationRequest request = new(GeolocationAccuracy.Medium, TimeSpan.FromSeconds(10));

            _cancelTokenSource = new CancellationTokenSource();

            location = await Geolocation.Default.GetLocationAsync(request, _cancelTokenSource.Token);


        }
        // Catch one of the following exceptions:
        //   FeatureNotSupportedException
        //   FeatureNotEnabledException
        //   PermissionException
        catch (Exception ex)
        {
            // Unable to get location
        }
        finally
        {
            _isCheckingLocation = false;
        }
    }

    public void CancelRequest()
    {
        if (_isCheckingLocation && _cancelTokenSource != null && _cancelTokenSource.IsCancellationRequested == false)
            _cancelTokenSource.Cancel();
    }

    public ObservableCollection<LocationItem> RequestedLocations { get; set; }

    public LocationsView()
    {

        if (DeviceInfo.Platform != DevicePlatform.Unknown)
        {
            InitializeComponent();
        }

        BindingContext = this;

        Locations ??= new ObservableCollection<LocationItem>();

        RequestedLocations = new ObservableCollection<LocationItem>();
        if (Locations != null)
        {
            foreach (LocationItem i in Locations)
            {
                if (i.State == 0)
                {
                    RequestedLocations.Add(i);
                }
            }
        }

        GetCurrentLocation();
    }

    public void OnSelectedChanged(object sender, EventArgs e)
    {

        var picker = (Picker)sender;
        int selectedIndex = picker.SelectedIndex;

        RequestedLocations.Clear();

        foreach (LocationItem i in Locations)
        {
            if (i.State == (UtilityState)selectedIndex)
            {
                RequestedLocations.Add(i);
            }
        }
    }
}