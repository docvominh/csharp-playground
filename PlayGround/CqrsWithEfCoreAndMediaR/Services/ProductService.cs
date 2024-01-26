using CqrsWithEfCoreAndMediaR.Data.Cqrs.Command;
using CqrsWithEfCoreAndMediaR.Data.Cqrs.Query;
using CqrsWithEfCoreAndMediaR.Data.Entity;
using MediatR;

namespace CqrsWithEfCoreAndMediaR.Services;

public class ProductService(ISender sender)
{
    public Task<List<Product>> GetAllProduct()
    {
        return sender.Send(new GetProductQuery());
    }

    public Task<List<Product>> CreateProduct(Product product)
    {
        sender.Send(new CreateProductCommand(product));
        return sender.Send(new GetProductQuery());
    }
}
