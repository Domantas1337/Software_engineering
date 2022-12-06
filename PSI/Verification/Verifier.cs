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

        public static TResult OnNetworkAccess<TArg, TResult>(Func<TArg, TResult> func, TArg arg) {
            if (Connectivity.Current.NetworkAccess != NetworkAccess.Internet)
            {
                Debug.WriteLine("---> No internet access...");
                return default;
            }
            return func.Invoke(arg);
        }

        public static TResult OnNetworkAccess<TResult>(Func<TResult> func)
        {
            if (Connectivity.Current.NetworkAccess != NetworkAccess.Internet)
            {
                Debug.WriteLine("---> No internet access...");
                return default;
            }
            return func.Invoke();
        }
    }
}
