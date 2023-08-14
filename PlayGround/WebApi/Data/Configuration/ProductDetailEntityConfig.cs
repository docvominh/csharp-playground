using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApi.Data.Entity;

namespace WebApi.Data.Configuration;

public class ProductDetailEntityConfig : IEntityTypeConfiguration<ProductDetailEntity>
{
    public void Configure(EntityTypeBuilder<ProductDetailEntity> builder)
    {
        builder.ToTable("ProductDetails");
        builder.Property(e => e.ProductId);
        builder.Property(e => e.Comment).IsRequired().HasMaxLength(255);

        builder.HasKey(e => new { e.ProductId }).HasName("PK_ProductDetails");

        builder
            .HasOne(e => e.Product)
            .WithOne(e => e.ProductDetail)
            .HasForeignKey<ProductDetailEntity>(e => e.ProductId)
            .HasConstraintName("FK_ProductId");
    }
}