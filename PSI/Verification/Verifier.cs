using __XamlGeneratedCode__;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PSI.Verification
{
    public static class Verifier
    {
        public static TResult OnNetworkAccess<T, TResult>(T arg, Func<T, TResult> func) {
            if (Connectivity.Current.NetworkAccess != NetworkAccess.Internet)
            {
                Debug.WriteLine("---> No internet access...");
                return default;
            }
            return func.Invoke(arg);
        }
    }
}
