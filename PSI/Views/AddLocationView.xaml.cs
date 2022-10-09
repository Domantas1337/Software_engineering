using PSI.FileManagers;
using PSI.Models;

namespace PSI.Views;

[QueryProperty(nameof(LocationItem), "Item")]
public partial class AddLocationView : ContentPage
{

    public LocationItem locationItem;
    private string _street, _city;
    private double _longitude, _latitude;
    private UtilityState _state;

    public string Street
    { 
        get => _street; 
        set{
            _street = value;
        }
    }
    public string City
    {
        get => _city;
        set
        {
            _city = value;
        }
    }
    public double Longitude
    {
        get => _longitude;
        set
        {
            _longitude = value;
        }
    }
    public double Latitude
    {
        get => _latitude;
        set
        {
            _latitude = value;
        }
    }

    public AddLocationView()
	{
		InitializeComponent();
	    BindingContext = this;
    }
    void OnSelectedChanged(object sender, EventArgs e)
    {
        var picker = (Picker)sender;
        int selectedIndex = picker.SelectedIndex;

        if (selectedIndex != -1)
        {
            _state = (UtilityState) selectedIndex;
        }
    }
    async void OnSaveButtonClicked(object sender, EventArgs e)
    {
        LocationItem locatioItem = new LocationItem() {  
            State = _state, 
            Street = this.Street, 
            City = this.City,
            Longitude = this.Longitude,
            Latitude = this.Latitude
        };
        locatioItem.write();

        await Shell.Current.GoToAsync("..");
    }

    async void OnDeleteButtonClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("..");
    }

    async void OnCancelButtonClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("..");
    }


}