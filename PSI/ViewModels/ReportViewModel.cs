using CommunityToolkit.Maui.Core.Extensions;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.VisualBasic;
using PSI.FileManagers;
using PSI.Generators;
using PSI.Models;
using PSI.Views;
using System.Collections.ObjectModel;
using System.Linq;

namespace PSI.ViewModels
{
    public partial class ReportViewModel : ObservableObject
    {
        string[] getReport; 
        string _report;
        public string Report
        {
            get { return _report; }
            set
            {

                _report = value;


                string tempString = string.Empty;
                Debug.WriteLine(_report);

                getReport = _report.Split(
                    new string[] { "\r\n", "\r", "\n" },
                    StringSplitOptions.None
                );

                Debug.WriteLine(getReport.Length);


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

        public ReportViewModel()
        {
            Items = new ObservableCollection<ReportItem>();

            List<ReportItem> reportItems = JSONFileManager<ReportItem>.Read(Constants.ReportsFilePath);
            reportItems.Sort();

            for(int i = 0; i < reportItems.Count; i++)
            {
                ReportItem temporaryItem = reportItems[i];
                temporaryItem.ImageName = Constants.CurrentAssemblyPath + @"\" + temporaryItem.ImageName;
                Items.Add(temporaryItem);
            }

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
        async void Add()
        {
            ReportItem reportItem = new()
            {
                Day = date.Day,
                Month = date.Month,
                Year = date.Year,
                ID = IDGenerator.GenerateID(),
                Title = this.ReportTitle,
                Report = getReport,
                ImageName = this.FileName
            };

            Items.Add(reportItem);

            JSONFileManager<ReportItem>.Write(Constants.ReportsFilePath, reportItem);

            Report = string.Empty;
            ReportTitle = string.Empty;

            SortItems();

            await Shell.Current.GoToAsync("..");
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
