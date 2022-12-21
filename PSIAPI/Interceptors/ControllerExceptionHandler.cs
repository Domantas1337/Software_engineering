using Castle.DynamicProxy;

namespace PSIAPI.Interceptors
{
    public class ControllerExceptionHandler : Attribute, IInterceptor
    {
        public void Intercept(IInvocation invocation)
        {
            try
            {

            }
            catch (Exception ex)
            {
                Console.WriteLine("woah");
            }
        }
    }
}
