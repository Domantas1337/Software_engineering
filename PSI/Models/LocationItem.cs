using PSI.States;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PSI.Models
{

    public struct LocationItem{
        public UtilityState State { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
    }
}
