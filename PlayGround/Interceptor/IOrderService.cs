namespace Interceptor;

public interface IOrderService
{
    public Task CreateOrderAsync(Order order);
}

public class OrderService : IOrderService
{
    public async Task CreateOrderAsync(Order order)
    {
        await Task.Delay(1000);
        Console.WriteLine($"Order created. ProductId: {order.ProductId}, Quantity: {order.Quantity}");
    }
}

public record Order(int ProductId, int Quantity);