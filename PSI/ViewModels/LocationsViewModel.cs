using CommunityToolkit.Mvvm.ComponentModel;
using PSI.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PSI.ViewModels
{
    public partial class LocationsViewModel : IQueryAttributable
    {
        public List<LocationItem> Items { get; private set; }
        public List<Location> Coords { get; private set; }
        public List<string> Streets { get; private set; }

        public List<string> Cities { get; private set; }

        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            Items = (List<LocationItem>)query["Locations"];
            
            foreach (var item in Items)
            {
                Coords.Add(new Location((double)item.Latitude, (double)item.Latitude));
                Streets.Add(item.Street);
                Cities.Add(item.City);
            }
        }
    }
}
