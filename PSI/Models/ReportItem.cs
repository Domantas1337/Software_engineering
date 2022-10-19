using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace PSI.Models
{
    public struct ReportItem : IComparable<ReportItem>
    {
        public int Year { get; set; }
        public int Month { get; set; }
        public int Day { get; set; }
        public string ID { get; set; }
        public string Title { get; set; }
        public string Report { get; set; }
        public string ImageName { get; set;  }

        public int CompareTo(ReportItem other)
        {
            return this.Title.CompareTo(other.Title);
        }
    }
}
