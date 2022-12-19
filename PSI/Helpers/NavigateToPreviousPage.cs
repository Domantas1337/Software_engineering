using PSI.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PSI.Helpers
{
    public static class NavigateToPreviousPage
    {
        public static async void NavigateBack()
        {
            await Shell.Current.GoToAsync("..");
        }
    }
}
