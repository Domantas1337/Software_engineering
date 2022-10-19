using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PSI.Generators
{
    public static class IDGenerator
    {   
        public static string GenerateID()
        {
            return Guid.NewGuid().ToString("N");
        }
    }
}
