using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PSI.Models
{
    class LogEventArgs : EventArgs
    {
        public LogItem LogItem;

        public LogEventArgs(LogItem logItem)
        { 
            LogItem = logItem;
        }
    }
}
