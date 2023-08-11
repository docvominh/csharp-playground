using EntityFrameWork.Database.Entity;
using Microsoft.EntityFrameworkCore;

namespace EntityFrameWork.Database;

public class ApplicationDbContext : DbContext
{
    public DbSet<ProductEntity> Products { get; set; } = null!;
    public DbSet<ProviderEntity> Providers { get; set; } = null!;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(
            @"Server=localhost;User ID=sa;Password=Hello12#;Database=EntityFrameWork;trustServerCertificate=True;");
    }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);


        modelBuilder.Entity<ProductEntity>(entity =>
        {
            entity.ToTable("Products");
            entity.HasKey(e => new { e.Id });
            entity.Property(e => e.Name).IsRequired().HasMaxLength(255);
            entity.Property(e => e.Category).HasMaxLength(100);
            entity.Property(e => e.Price).HasColumnType("money");
            entity.Property(e => e.Manufacture).HasMaxLength(255);
        });

        modelBuilder.Entity<ProductEntity>().HasOne(e => e.ProductDetail)
            .WithOne(e => e.Product)
            .HasForeignKey<ProductDetailEntity>(e => e.ProductId);

        modelBuilder.Entity<ProductEntity>()
            .HasMany(e => e.ProductProviders)
            .WithOne(e => e.Product)
            .HasForeignKey(e => e.ProductId);

        modelBuilder.Entity<ProductDetailEntity>(entity =>
        {
            entity.ToTable("ProductDetails");
            entity.HasKey(e => new { e.ProductId });
        });

        modelBuilder.Entity<ProviderEntity>(entity =>
        {
            entity.ToTable("Providers");
            entity.HasKey(e => new { e.Id });
        });
        modelBuilder.Entity<ProductProviderEntity>(entity =>
        {
            entity.ToTable("ProductProvider");
            entity.HasKey(e => new { e.ProductId, e.ProviderId });
        });
    }
}