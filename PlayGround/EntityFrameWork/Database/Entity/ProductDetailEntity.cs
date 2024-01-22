using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EntityFrameWork.Database.Entity;

public class ProductDetailEntity
{
    public int ProductId { get; set; }
    public string? Comment { get; set; }
    public virtual ProductEntity Product { get; set; } = null!;
}

public class ProductDetailEntityConfig : IEntityTypeConfiguration<ProductDetailEntity>
{
    public void Configure(EntityTypeBuilder<ProductDetailEntity> builder)
    {
        builder.ToTable("ProductDetails");
        builder.Property(e => e.ProductId).HasColumnType("int");
        builder.Property(e => e.Comment).IsRequired().HasMaxLength(255);

        builder.HasKey(e => new { e.ProductId }).HasName("PK_ProductDetails");

        builder
            .HasOne(e => e.Product)
            .WithOne(e => e.ProductDetail)
            .HasForeignKey<ProductDetailEntity>(e => e.ProductId)
            .HasConstraintName("FK_ProductId");
    }
}