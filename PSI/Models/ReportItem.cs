using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PSI.Models
{
    public struct ReportItem
    {
        public string _image;
        public string ID { get; set; }
        public string Title { get; set; }
        public string Report { get; set; }

        public string ImageName
        {
            get
            {
                return _image;
            }
            set
            {
                _image = value;
            }
        }


        public string GetImagePath()
        {
            return Constants.currentAssemblyPath + @"\" + _image;
        }
        public override string ToString()
        {
            return Title;
        }
    }
}
