using Microsoft.AspNetCore.Mvc;
using WebApi.Models;
using WebApi.Services;

namespace WebApi.Controllers;

[ApiController]
[Route("api/products")]
public class ProductController : ControllerBase
{
    private readonly ILogger<ProductController> _logger;
    private readonly IProductService _service;

    public ProductController(ILogger<ProductController> logger, IProductService service)
    {
        _logger = logger;
        _service = service;
    }

    [HttpGet("{id:int}")]
    public async Task<Product> Get(int id)
    {
        return await _service.FindById(id);
    }

    [HttpGet]
    public async Task<List<Product>> GetAllProducts()
    {
        return await _service.FindAll();
    }

    [HttpPost]
    public async Task<Product> Save(Product product)
    {
        return await _service.Save(product);
    }

    [HttpPut]
    public async Task<Product> Update(Product product)
    {
        return await _service.Update(product);
    }

    [HttpDelete("{id:int}")]
    public async Task Delete(int id)
    {
        _logger.LogInformation("Delete entity with id {Id}", id);
        await _service.Delete(id);
    }
}