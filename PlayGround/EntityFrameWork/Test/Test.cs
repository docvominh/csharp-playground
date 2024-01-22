using EntityFrameWork.Database;
using EntityFrameWork.Database.Entity;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace EntityFrameWork.Test;

public class Test
{
    private readonly AppDbContext? _context;

    public Test()
    {
        _context = new AppDbContext();
        _context.Products.ExecuteDelete();
        _context.Providers.ExecuteDelete();
    }

    [Fact]
    public void TestInsertProduct()
    {
        // Add product
        var newProduct = new ProductEntity()
        {
            Name = "Asus 202",
            Category = "Computer",
            Price = new decimal(500.5),
            Manufacture = "Asus",
            ProductDetail = new ProductDetailEntity
            {
                Comment = "Cheap"
            }
        };

        _context.Products.Add(newProduct);
        _context.SaveChanges();

        var product = _context.Products
            .Include(productEntity => productEntity.ProductDetail)
            .SingleOrDefault(e => e.Id == newProduct.Id);

        product.Should().NotBeNull();
        product.Name.Should().Be("Asus 202");
        product.ProductDetail.Comment.Should().Be("Cheap");
    }

    [Fact]
    public void TestInsertProductWithProvider()
    {
        if (_context is null)
        {
            throw new NullReferenceException();
        }
        // Add provider

        var providers = new List<ProviderEntity>()
        {
            new()
            {
                Name = "Amazon",
                Address = "123 USA"
            },
            new()
            {
                Name = "EBAY",
                Address = "123 SSA"
            }
        };

        _context.Providers.AddRange(providers);
        _context.SaveChanges();

        // Add product
        var newProduct = new ProductEntity()
        {
            Name = "Asus 202",
            Category = "Computer",
            Price = new decimal(500.5),
            Manufacture = "Asus",
            ProductDetail = new ProductDetailEntity
            {
                Comment = "Cheap"
            }
        };

        _context.Products.Add(newProduct);
        _context.SaveChanges();

        newProduct.Providers = providers;

        _context.Products.Update(newProduct);
        _context.SaveChanges();

        var product = _context.Products
            .Include(productEntity => productEntity.ProductDetail)
            .Include(productEntity => productEntity.Providers)
            .SingleOrDefault(e => e.Id == newProduct.Id);

        product.Should().NotBeNull();
        product.Name.Should().Be("Asus 202");
        product.ProductDetail.Comment.Should().Be("Cheap");
        product.Providers?.Count.Should().Be(2);
    }
}