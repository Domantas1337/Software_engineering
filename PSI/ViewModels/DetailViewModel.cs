using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PSI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace PSI.ViewModels
{
    [QueryProperty("AReport", "AReport")]
    public partial class DetailViewModel : ObservableObject
    {
        [ObservableProperty]
        string[] aReport;

        public DateTime currentTime = DateTime.Now;
    }
}
