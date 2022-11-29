using PSI.States;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PSI.Models
{

    public class LocationItem
    {
        [Key]
        public string ID { get; set; }
        public UtilityState State { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        public Location Position { get; set; }


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
