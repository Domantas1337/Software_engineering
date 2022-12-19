using PSI.FileManagers;
using PSI.Helpers;
using PSI.Models;
using PSI.Services;
using PSI.States;

namespace PSI.Views.ManageLocation;

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

    private readonly ILocationService _dataService;

    public AddLocationView(ILocationService dataService)
    {
        if (DeviceInfo.Platform != DevicePlatform.Unknown)
        {
            InitializeComponent();
            organicButton.Clicked += OnButtonClicked;
            organicButton.Clicked += (obj, args) => selectedBin = BinSelectionState.Organic;
            plasticButton.Clicked += OnButtonClicked;
            plasticButton.Clicked += (obj, args) => selectedBin = BinSelectionState.Plastic;
            BindingContext = this;
        }
        _dataService = dataService;
  
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
    public async void OnSaveButtonClicked(object sender, EventArgs e)
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
            if (DeviceInfo.Platform != DevicePlatform.Unknown)
            {
                errorMsg.Text = ErrorBody;
                ErrorBody = "Invalid:\n";

            }
        }
        else
        {
            LocationItem locationItem = new()
            {
                ID = Generators.IDGenerator.GenerateID(),
                State = this.State,
                Street = this.Street,
                City = this.City,
                Longitude = Longitude,
                Latitude = Latitude
                
            };

            await _dataService.AddLocationItemAsync(locationItem);

            Verification.PlatformVerification.IsPlatformUnknown(NavigateToPreviousPage.NavigateBack);

        }
    }

    
    async void OnDeleteButtonClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(DeleteLocationView));
    }

    async void OnCancelButtonClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("..");
    }

}