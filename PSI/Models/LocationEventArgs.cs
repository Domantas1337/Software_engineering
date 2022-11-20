using Microsoft.Maui.Maps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PSI.Models
{
    public class LocationEventArgs : EventArgs
    {
        public LocationItem Location;
        public double NearestLocation;

        public LocationEventArgs(LocationItem location, double nearestLocation)
        {
            Location = location;
            NearestLocation = nearestLocation;
        }
    }
}
