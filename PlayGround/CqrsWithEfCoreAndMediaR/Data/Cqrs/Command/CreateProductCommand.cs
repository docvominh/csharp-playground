using CqrsWithEfCoreAndMediaR.Data.Entity;
using MediatR;

namespace CqrsWithEfCoreAndMediaR.Data.Cqrs.Command;

public record CreateProductCommand(Product Product) : IRequest;

public class CreateProductHandler(AppDbContext dbContext) : IRequestHandler<CreateProductCommand>
{
    public async Task Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        dbContext.Products.Add(request.Product);
        await dbContext.SaveChangesAsync(cancellationToken);
    }
}
