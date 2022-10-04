using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PSI
{
    enum UtilityState
    {
        TrashCan,
        Taromat,
        Something
    }
    internal class PageState
    {
        public static UtilityState utilityState = UtilityState.TrashCan;
    }

}
