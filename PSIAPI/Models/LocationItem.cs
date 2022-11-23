using PSIAPI.States;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PSIAPI.Models
{

    public class LocationItem {
        [Key]
        public int? Id { get; set; }
        public UtilityState State { get; set; }
        public string? Street { get; set; }
        public string? City { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
    }
}
