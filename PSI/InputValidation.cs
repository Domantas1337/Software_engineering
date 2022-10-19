using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PSI
{
    internal static class InputValidation
    {

        public static bool isEmailExtension(this string email)
        {
            if(Regex.IsMatch(email, @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", RegexOptions.IgnoreCase)){
                return true;
            }
            return false;
        }
        public static bool CensorTextExtension(this string text)
        {
            if (Regex.IsMatch(text, "(?i)(shit)|(fuc(k)?)"))
            {
                return true;
            }
            return false;
        }
    }
}
