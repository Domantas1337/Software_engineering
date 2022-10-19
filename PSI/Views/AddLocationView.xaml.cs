using CommunityToolkit.Maui.Converters;
using PSI.FileManagers;
using PSI.Models;
using PSI.State;

namespace PSI.Views;

[QueryProperty(nameof(LocationItem), "Item")]
public partial class AddLocationView : ContentPage
{
    public String ErrorBody = "Invalid:\n";
    public LocationItem locationItem;
    public UtilityState State = 0;

    public string Street { get; set; }
    public string City { get; set; }
    public string LongitudeText { get; set; }
    public double Longitude { get; set; }
    public string LatitudeText { get; set; }
    public double Latitude { get; set; }
    public AddLocationView()
	{
		InitializeComponent();
	    BindingContext = this;
    }
    void OnSelectedChanged(object sender, EventArgs e)
    {
        var picker = (Picker) sender;
        int selectedIndex = picker.SelectedIndex;
        State = (UtilityState) picker.SelectedIndex;
    }
    async void OnSaveButtonClicked(object sender, EventArgs e)
    {
        bool errored = false;
        if (String.IsNullOrEmpty(Street))
        {
            ErrorBody += "\n* street\n";
            errored = true;
        }
        if (String.IsNullOrEmpty(City))
        {
            ErrorBody += "\n* city\n";
            errored = true;
        }
        if (String.IsNullOrEmpty(LongitudeText))
        {
            ErrorBody += "\n* longitude\n";
            errored = true;
        }
        else
        {
            try
            {
                Longitude = Double.Parse(LongitudeText);
            }
            catch (Exception exc) {
                ErrorBody += "\n* longitude\n";
                errored = true;
                Debug.Write(exc.Message);
            }
        }
        if (String.IsNullOrEmpty(LatitudeText))
        {
            ErrorBody += "\n* latitude\n";
            errored = true;
        }
        else
        {
            try
            {
                Latitude = Double.Parse(LatitudeText);
            }
            catch (Exception exc)
            {
                ErrorBody += "\n* latitude\n";
                errored = true;
                Debug.Write(exc.Message);
            }
        }
        if (errored)
        {
            errorMsg.Text = ErrorBody;
            ErrorBody = "Invalid:\n";
        }
        else
        {
            LocationItem locationItem = new LocationItem()
            {
                State = this.State,
                Street = this.Street,
                City = this.City,
                Longitude = Longitude,
                Latitude = Latitude
            };
            JSONFileManager<LocationItem>.Write(
                                        filePath: Constants.LocationsFilePath,
                                        item: locationItem
                                        );

            await Shell.Current.GoToAsync("..");
        }
    }

    async void OnDeleteButtonClicked(object sender, EventArgs e)
    {
        List<LocationItem> list = JSONFileManager<LocationItem>.Read(Constants.LocationsFilePath);

        /*var newList = list.Where(l => l.Longitude != _longitude)
                          .Select(l => l);*/

        var newList = from item in list
                      where item.Longitude != Longitude
                            && item.Latitude != Longitude
                      select item;

        JSONFileManager<LocationItem>.Write(
                                    filePath: Constants.LocationsFilePath,
                                    items: newList.ToList()
                                    );
        await Shell.Current.GoToAsync("..");
    }

    async void OnCancelButtonClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("..");
    }


}