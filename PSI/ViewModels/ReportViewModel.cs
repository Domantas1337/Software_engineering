using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.VisualBasic;
using PSI.FileManagers;
using PSI.Generators;
using PSI.Models;
using PSI.Views;
using System.Collections.ObjectModel;

namespace PSI.ViewModels
{
    public partial class ReportViewModel : ObservableObject
    {

        public ReportViewModel()
        {
            Items = new ObservableCollection<ReportItem>();

            List<ReportItem> reportItems = JSONFileManager<ReportItem>.read(Constants.reportsFilePath);
            reportItems.Sort();

            for(int i = 0; i < reportItems.Count; i++)
            {
                ReportItem temporaryItem = reportItems[i];
                temporaryItem.ImageName = Constants.currentAssemblyPath + @"\" + temporaryItem.ImageName;
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
        string report;

        [RelayCommand]
        async void Add()
        {
            ReportItem reportItem = new ReportItem()
            {
                Day = date.Day,
                Month = date.Month,
                Year = date.Year,
                ID = IDGenerator.generateID(),
                Title = this.ReportTitle,
                Report = this.Report,
                ImageName = this.FileName
            };

            Items.Add(reportItem);

            JSONFileManager<ReportItem>.write(Constants.reportsFilePath, reportItem);

            Report = string.Empty;
            ReportTitle = string.Empty;

            SortItems();

            await Shell.Current.GoToAsync("..");
        }

        [RelayCommand]
        void SortItems()
        {
            List<ReportItem> sorted = Items.OrderBy(x => x).ToList();
            for (int i = 0; i < sorted.Count(); i++)
                Items.Move(Items.IndexOf(sorted[i]), i);
        }

        [RelayCommand]
        async void Pick()
        {
            FileResult photo = await MediaPicker.Default.PickPhotoAsync();

            if (photo != null)
            {
                // save the file into local storage
                string localFilePath = Path.Combine(Constants.currentAssemblyPath, photo.FileName);


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

        [RelayCommand]
        async Task Details(string s)
        {
            await Shell.Current.GoToAsync($"{nameof(ReportDetailPage)}?Text={s}");
        }

        public string FileName { get; set; }
    }
}
