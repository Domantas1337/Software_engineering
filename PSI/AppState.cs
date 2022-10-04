using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PSI
{
    public enum UtilityState
    {
        TrashCan,
        Taromat,
        Litter
    }
    internal class PageState
    {
        public static UtilityState utilityState = UtilityState.TrashCan;
    }

}
