using PSI.State;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PSI.Models
{

    public struct LocationItem : IComparer<LocationItem>{
        public UtilityState State { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }

        public int Compare(LocationItem x, LocationItem y)
        {
            if (x.Longitude != y.Longitude)
                return 0;
            if (x.Latitude != y.Latitude)
                return 0;
            return 1;
        }
    }
}
