using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace PSIAPI.Models
{
    public class ReportItem
    {
        [Key]
        public string? ID { get; set; }
        public string Date { get; set; }
        public string? Title { get; set; }
        public string? Report { get; set; }
    }
}
