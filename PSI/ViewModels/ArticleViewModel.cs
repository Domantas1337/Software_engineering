using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PSI.FileManagers;
using PSI.Generators;
using PSI.Models;
using System.Collections.ObjectModel;

namespace PSI.ViewModels
{
    public partial class ArticleViewModel : ObservableObject
    {

        public ArticleViewModel()
        {
            Items = new ObservableCollection<ReportItem>();

            List<ReportItem> reportItems = JSONFileManager<ReportItem>.read(Constants.reportsFilePath);


            if (reportItems != null)
            {
                foreach (ReportItem i in reportItems)
                {
                    ReportItem j = i;
                    j.ImageName = Constants.currentAssemblyPath + @"\" + i.ImageName;
                    Items.Add(j);
                }
            }
        }

        [ObservableProperty]
        ObservableCollection<ReportItem> items;

        [ObservableProperty]
        string reportTitle;

        [ObservableProperty]
        string report;

        [RelayCommand]
        async void Add()
        {
            ReportItem reportItem = new ReportItem()
            {
                ID = IDGenerator.generateID(),
                Title = this.ReportTitle,
                Report = this.Report,
                ImageName = this.FileName
            };

            Items.Add(reportItem);

            JSONFileManager<ReportItem>.write(Constants.reportsFilePath, reportItem);

            Report = string.Empty;
            ReportTitle = string.Empty;

            await Shell.Current.GoToAsync("..");
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

        public string FileName { get; set; }
    }
}
