using Castle.DynamicProxy;

namespace Interceptor;

public class OrderCreateInterceptor : IInterceptor
{
    public void Intercept(IInvocation invocation)
    {
        Console.WriteLine("Ready to create order");
        invocation.Proceed();
        Console.WriteLine("Order created");
    }
}