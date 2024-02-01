using Marten;
using Marten.Schema;
using Microsoft.AspNetCore.Mvc;

namespace MartenEventSource.Controllers;

[ApiController]
[Route("[controller]")]
public class ProductController : ControllerBase
{
    private readonly IDocumentStore _store;

    public ProductController(IDocumentStore store)
    {
        _store = store;
    }

    [HttpGet("{id:guid}")]
    public async Task<Product> Get(Guid id, CancellationToken cancellationToken)
    {
        await using var session = _store.LightweightSession();
        return await session.LoadAsync<Product>(id, cancellationToken) ?? throw new InvalidOperationException();
    }

    [HttpGet]
    public async Task<IReadOnlyCollection<Product>> GetAllProducts(Guid id, CancellationToken cancellationToken)
    {
        await using var session = _store.LightweightSession();
        return await session.LoadManyAsync<Product>(cancellationToken, id) ??
               throw new InvalidOperationException();
    }

    [HttpPost]
    public async Task<Product> Save(Product product)
    {
        // Open a session for querying, loading, and updating documents
        await using var session = _store.LightweightSession();

        session.Store(product);

        await session.SaveChangesAsync();

        return product;
    }

    [HttpPut]
    public async Task<Product> Update(Product product)
    {
        // Open a session for querying, loading, and updating documents
        await using var session = _store.LightweightSession();

        session.Update(product);

        await session.SaveChangesAsync();

        return product;
    }
}


// [DatabaseSchemaName("products")]
// [DocumentAlias("products")]
public class Product
{
    [Identity]
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public string? Category { get; set; }
    public decimal? Price { get; set; }
    public string? Manufacture { get; set; }
}
