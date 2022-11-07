using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace PSI.Models
{
    public class ReportItem : IComparable<ReportItem>
    {
        public int Year { get; set; }
        public int Month { get; set; }
        public int Day { get; set; }
        public string ID { get; set; }
        public string Title { get; set; }
        public string[] Report { get; set; }
        public string ImageName { get; set;  }

        public int CompareTo(ReportItem other)
        {
            if (this.Title == null)
            {
                return 1;
            }
            if (other.Title == null)
            {
                return -1;
            }
            return this.Title.CompareTo(other.Title);
        }
    }
}
