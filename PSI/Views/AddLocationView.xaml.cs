using PSI.FileManagers;
using PSI.Models;
using PSI.Services;
using PSI.States;

namespace PSI.Views;

[QueryProperty(nameof(LocationItem), "Item")]
public partial class AddLocationView : ContentPage
{
    public String ErrorBody = "Invalid:\n";
    public LocationItem locationItem;
    public UtilityState State = 0;
    public BinSelectionState selectedBin;

    public string Street { get; set; }
    public string City { get; set; }
    public string LongitudeText { get; set; }
    public double Longitude { get; set; }
    public string LatitudeText { get; set; }
    public double Latitude { get; set; }
    public string Details { get; set; }

    // TODO: make more models that save specific selections

    private readonly IRestService _dataService;

    public AddLocationView(IRestService dataService)
    {
        InitializeComponent();

        _dataService = dataService;
        BindingContext = this;
        organicButton.Clicked += OnButtonClicked;
        organicButton.Clicked += (obj, args) => selectedBin = BinSelectionState.Organic;
        plasticButton.Clicked += OnButtonClicked;
        plasticButton.Clicked += (obj, args) => selectedBin = BinSelectionState.Plastic;
    }

    void OnButtonClicked(object sender, EventArgs e)
    {
        ResetButtons(sender, e);
        MakeButtonClicked(sender, e);
    }

    void MakeButtonClicked(object sender, EventArgs e)
    {
        ((ImageButton)sender).Scale = 0.8;
    }

    void ResetButtons(object sender, EventArgs e)
    {
        organicButton.Scale = 1;
        plasticButton.Scale = 1;
    }

    void OnSelectedChanged(object sender, EventArgs e)
    {
        var picker = (Picker)sender;
        State = (UtilityState)picker.SelectedIndex;
        if (binDetails == null)
        {
            return;
        }
        binDetails.IsVisible = false;
        details.IsVisible = false;
        if (State == UtilityState.TrashCan)
        {
            binDetails.IsVisible = true;
        }
        else if (State == UtilityState.Litter || State == UtilityState.Taromat)
        {
            details.IsVisible = true;
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
            catch (Exception exc)
            {
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
                Id = (int)((int)Longitude + Latitude),
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


            Debug.WriteLine("---> Add new Item");
            await _dataService.AddLocationItemAsync(locationItem);

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