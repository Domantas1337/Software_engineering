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
    public VerticalStackLayout vsl = new()
    {
        new Label()
        {
            Text = "Details"
        },
        new Editor()
        {
            Placeholder = "Enter details about littering here",
            HeightRequest = 250
        }
    };
    public HorizontalStackLayout hsl = new()
    {
        HeightRequest = 250
    };
    public VerticalStackLayout vslImages = new()
    {            
        new Label()
            {
                Text = "Details"
            }
    };

    public AddLocationView()
	{
		InitializeComponent();
	    BindingContext = this;
        ImageButton organicButton = new()
        {
            Source = "organic.png",
            Aspect = Aspect.Fill,
        };
        hsl.Add(organicButton);
        vslImages.Add(hsl);
    }
    void OnSelectedChanged(object sender, EventArgs e)
    {
        var picker = (Picker) sender;
        State = (UtilityState) picker.SelectedIndex;
        Debug.WriteLine("changed");
        if (extraContents != null)
        {
            extraContents.Remove(vsl);
            if (State.Equals(UtilityState.Litter))
            {
                extraContents.Add(vsl);
            }
            if (State.Equals(UtilityState.TrashCan))
            {
                extraContents.Add(vslImages);
            }
        }
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
            LocationItem locationItem = new()
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
                            || item.Latitude != Longitude
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