// See https://aka.ms/new-console-template for more information

using Castle.DynamicProxy;
using Interceptor;

Console.WriteLine("Hello, World!");

OrderCreateInterceptor orderCreateInterceptor = new ();

ProxyGenerator proxyGenerator = new ();
var orderService = proxyGenerator.CreateInterfaceProxyWithTarget<IOrderService>(new OrderService(), orderCreateInterceptor);

await orderService.CreateOrderAsync(new Order(1, 1));