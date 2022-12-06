using PSI.States;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PSI.Models
{

    public class LocationItem : IComparable<LocationItem>
    {
        [Key]
        public string ID { get; set; }
        public UtilityState State { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        public Location Position { get; set; }
         

        public int CompareTo(LocationItem item)
        {

            if (!this.ID.Equals(item.ID))
                return 0;
            if (this.State != item.State)
                return 0;
            if (!this.Street.Equals(item.Street))
                return 0;
            if (!this.City.Equals(item.City))
                return 0;
            if (this.Longitude != item.Longitude)
                return 0;
            if (this.Latitude != item.Latitude)
                return 0;
            return 1;

        }
    }
}
