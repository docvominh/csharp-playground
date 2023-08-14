using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApi.Data.Entity;

namespace WebApi.Data.Configuration;

public class ProductEntityConfig : IEntityTypeConfiguration<ProductEntity>
{
    public void Configure(EntityTypeBuilder<ProductEntity> builder)
    {
        builder.ToTable("Products");
        builder.Property(e => e.Id).ValueGeneratedOnAdd();
        builder.Property(e => e.Name).IsRequired().HasMaxLength(255);
        builder.Property(e => e.Category).HasMaxLength(100);
        builder.Property(e => e.Price).HasColumnType("money");
        builder.Property(e => e.Manufacture).HasMaxLength(255);

        builder.HasKey(e => new { e.Id }).HasName("PK_Products");

        builder.HasOne(e => e.ProductDetail)
            .WithOne(e => e.Product)
            .HasForeignKey<ProductDetailEntity>(e => e.ProductId);


        // Create join table
        builder.HasMany(e => e.Providers).WithMany(e => e.Products)
            .UsingEntity("ProductProviders",
                l => l.HasOne(typeof(ProviderEntity)).WithMany()
                    .HasForeignKey("ProviderId").HasConstraintName("FK_Provider_ProviderId")
                    .HasPrincipalKey(nameof(ProviderEntity.Id)),
                r => r.HasOne(typeof(ProductEntity)).WithMany()
                    .HasForeignKey("ProductId").HasConstraintName("FK_Product_ProductId")
                    .HasPrincipalKey(nameof(ProductEntity.Id)),
                j => j.HasKey("ProductId", "ProviderId").HasName("PK_Product_Provider"));
    }
}