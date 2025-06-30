using EntityFrameworkMigration.Database;
using EntityFrameworkMigration.Database.Entity;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace EntityFrameworkMigration.Test;

public class ProductTagTest
{
    private readonly AppDbContext _context = DbContextFactory.CreateDbContext(nameof(ProductTagTest));

    [Fact]
    public void Save_Product_With_ManyTag()
    {
        if (_context is null)
        {
            throw new NullReferenceException();
        }

        var tags = new List<TagEntity>()
        {
            new ()
            {
                Name = "Computer"
            },
            new ()
            {
                Name = "Laptop"
            },
            new ()
            {
                Name = "Asus"
            }
        };

        _context.Tags.AddRange(tags);
        _context.SaveChanges();


        var exitedTags = _context.Tags.ToList();

        var product = new ProductEntity()
        {
            Name = "Asus 202",
            Category = "Computer",
            Price = new decimal(500.5),
            Manufacture = "Asus",
            ProductDetail = new ProductDetailEntity
            {
                Comment = "Cheap"
            },
            Tags = exitedTags
        };

        _context.Products.Add(product);
        _context.SaveChanges();

        var productEntity = _context.Products
            .Include(productEntity => productEntity.ProductDetail)
            .Include(productEntity => productEntity.Tags)
            .SingleOrDefault(e => e.Id == product.Id);

        productEntity.Should().NotBeNull();
        productEntity.Name.Should().Be("Asus 202");
        productEntity.ProductDetail.Comment.Should().Be("Cheap");
        productEntity.Tags.Should().HaveCount(3);


        var newProductEntity = _context.Products.Find(productEntity.Id);
        var newTags = _context.Tags.ToList();
        newProductEntity.Tags = newTags;

        newProductEntity.Tags.Add(
            new TagEntity()
            {
                Name = "New Tag"
            }
        );

        _context.Products.Update(newProductEntity);


        productEntity.Should().NotBeNull();
        productEntity.Name.Should().Be("Asus 202");
        productEntity.ProductDetail.Comment.Should().Be("Cheap");
        productEntity.Tags.Should().HaveCount(4);
    }
}