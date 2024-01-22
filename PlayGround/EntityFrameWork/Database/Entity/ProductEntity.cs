using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EntityFrameWork.Database.Entity;

public class ProductEntity
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Category { get; set; }
    public decimal? Price { get; set; }
    public string? Manufacture { get; set; }
    public virtual ProductDetailEntity? ProductDetail { get; set; }
    public virtual ICollection<TagEntity>? Tags { get; set; }
    public virtual ICollection<ProviderEntity>? Providers { get; set; }
}

public class ProductEntityConfig : IEntityTypeConfiguration<ProductEntity>
{
    public void Configure(EntityTypeBuilder<ProductEntity> builder)
    {
        builder.ToTable("Products");
        builder.Property(e => e.Id).HasColumnType("int").ValueGeneratedOnAdd();
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

        builder.HasMany(e => e.Tags).WithMany(e => e.Products)
            .UsingEntity("ProductTags",
                l => l.HasOne(typeof(TagEntity)).WithMany()
                    .HasForeignKey("TagId").HasConstraintName("FK_Product_Tag_TagId")
                    .HasPrincipalKey(nameof(TagEntity.Id)),
                r => r.HasOne(typeof(ProductEntity)).WithMany()
                    .HasForeignKey("ProductId").HasConstraintName("FK_Product_Tag_ProductId")
                    .HasPrincipalKey(nameof(ProductEntity.Id)),
                j => j.HasKey("ProductId", "TagId").HasName("PK_Product_Tag"));
    }
}