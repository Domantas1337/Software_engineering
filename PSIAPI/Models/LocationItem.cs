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
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public string Id { get; set; }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public UtilityState State { get; set; }
        public string? Street { get; set; }
        public string? City { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
    }
}
