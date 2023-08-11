using EntityFrameWork.Database;
using EntityFrameWork.Database.Entity;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace EntityFrameWork.Test;

[TestFixture]
public class Test
{
    private ApplicationDbContext? _context;

    [SetUp]
    public void SetUp()
    {
        _context = new ApplicationDbContext();
        _context.Products.ExecuteDelete();
        _context.Providers.ExecuteDelete();
    }

    [Test]
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

        Assert.That(product, Is.Not.Null);
        Assert.That(product?.Name, Is.EqualTo("Asus 202"));
        Assert.That(product?.ProductDetail?.Comment, Is.EqualTo("Cheap"));
        
    }
    [Test]
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

        newProduct.ProductProviders = new List<ProductProviderEntity>()
        {
            new()
            {
                Product = newProduct,
                Provider = providers[0]
            },
            new()
            {
                Product = newProduct,
                Provider = providers[1]
            }
        };

        _context.Products.Update(newProduct);
        _context.SaveChanges();
        
        var product = _context.Products
            .Include(productEntity => productEntity.ProductDetail)
            .Include(productEntity => productEntity.ProductProviders)
            .SingleOrDefault(e => e.Id == newProduct.Id);

        Assert.That(product, Is.Not.Null);
        Assert.That(product?.Name, Is.EqualTo("Asus 202"));
        Assert.That(product?.ProductDetail?.Comment, Is.EqualTo("Cheap"));
        Assert.That(product?.ProductProviders?.Count, Is.EqualTo(2));
        
    }
}