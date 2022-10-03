using PSI.FileManagers;
using PSI.Models;
using PSI.FileManagers;

namespace PSI.Views;

[QueryProperty(nameof(LocationItem), "Item")]
public partial class AddLocationView : ContentPage
{

    public LocationItem locationItem;
    bool _isNewItem;
    private string _street, _city;
    private double _longitude, _latitude;

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

    async void OnSaveButtonClicked(object sender, EventArgs e)
    {
        Console.WriteLine("aaaaaaa");
        WriteJSON newWriter = new WriteJSON( new LocationItem() { 
            Street = this.Street, 
            City = this.City,
            Longitude = this.Longitude,
            Latitude = this.Latitude
        } ) ;
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