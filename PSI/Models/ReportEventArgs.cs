using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PSI.Models
{
    internal class ReportEventArgs : EventArgs
    {
        public List<LogItem> LogItems;

        public ReportEventArgs(List<LogItem> logItems)
        {
            LogItems = logItems;
        }
    }
}
