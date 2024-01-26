using CqrsWithEfCoreAndMediaR.Data.Entity;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CqrsWithEfCoreAndMediaR.Data.Cqrs.Query;

public record GetProductQuery : IRequest<List<Product>>;

public class GetProductHandle(AppDbContext dbContext) : IRequestHandler<GetProductQuery, List<Product>>
{
    public async Task<List<Product>> Handle(GetProductQuery request, CancellationToken cancellationToken)
    {
        return await dbContext.Products.ToListAsync(cancellationToken);
    }
}
