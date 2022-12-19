using CommunityToolkit.Maui.Core.Extensions;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.VisualBasic;
using PSI.FileManagers;
using PSI.Generators;
using PSI.Helpers;
using PSI.Models;
using PSI.Services;
using PSI.Views;
using PSI.Views.ManageLocation;
using System.Collections.ObjectModel;
using System.Linq;

namespace PSI.ViewModels
{
    public partial class ReportViewModel : ObservableObject
    {

        // public EventHandler<ReportEventArgs> OnAddReportClicked;

        string[] getReport; 
        string _report;


        public string Report
        {
            get { return _report; }
            set
            {

                _report = value;

                if (_report.CensorTextExtension())
                {
                    InvalidInput = "Curse words are not allowed!";
                }
                else
                {
                    InvalidInput = "";
                }

            }
        }
        public string FileName { get; set; }

        private ReportRestService _reportRestService;
        public ReportViewModel(ReportRestService reportRestService)
        {
            Items = new ObservableCollection<ReportItem>();

            _reportRestService = reportRestService;

        }


        [ObservableProperty]
        ObservableCollection<ReportItem> items;

        [ObservableProperty]
        DateTime date;

        [ObservableProperty]
        string reportTitle;

        [ObservableProperty]
        string invalidInput;

        [RelayCommand]
        public async void Add()
        {
            ReportItem reportItem = new()
            {
                Date = DateTime.UtcNow.ToString(),
                ID = IDGenerator.GenerateID(),
                Title = this.ReportTitle,
                Report = _report
            };

            Items.Add(reportItem);

            // OnAddReportClicked();
            await _reportRestService.AddLocationItemAsync(reportItem);

            Report = string.Empty;
            ReportTitle = string.Empty;

            SortItems();

            Verification.PlatformVerification.IsPlatformUnknown(NavigateToPreviousPage.NavigateBack);
        }

        [RelayCommand]
        void SortItems()
        {
            Items = Items.OrderBy(x => x).ToObservableCollection();
        }


        [RelayCommand]
        async void Pick()
        {
            FileResult photo = await MediaPicker.Default.PickPhotoAsync();

            if (photo != null)
            {
                // save the file into local storage
                string localFilePath = Path.Combine(Constants.CurrentAssemblyPath, photo.FileName);


                using Stream sourceStream = await photo.OpenReadAsync();
                using FileStream localFileStream = File.OpenWrite(localFilePath);

                string imagePath = localFileStream.Name;

                string isPng = imagePath.Substring(imagePath.Length - 3, 3);

                Debug.WriteLine(isPng);
                Debug.WriteLine(imagePath);


                int i = imagePath.Length - 1;


                FileName = string.Empty;
                while (imagePath.ElementAt(i) != '\\')
                {
                    --i;
                }
                ++i;
                while (i < imagePath.Length)
                {
                    FileName += imagePath.ElementAt(i);
                    ++i;
                }


                Debug.WriteLine(FileName);

                await sourceStream.CopyToAsync(localFileStream);
            }
        }
    }
}
