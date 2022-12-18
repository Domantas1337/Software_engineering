using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SQLite.SQLite3;

namespace PSI.Verification
{
    public static class PlatformVerification
    {
        public static void IsPlatformUnknown(Action action)
        {
            if(DeviceInfo.Platform == DevicePlatform.Unknown)
            {
                return;
            }
            action.Invoke();
        }
    }

}
