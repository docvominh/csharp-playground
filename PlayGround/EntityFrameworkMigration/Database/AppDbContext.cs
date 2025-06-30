using EntityFrameworkMigration.Database.Entity;
using Microsoft.EntityFrameworkCore;

namespace EntityFrameworkMigration.Database;

public class AppDbContext : DbContext
{
    public DbSet<ProductEntity> Products { get; set; } = null!;

    public DbSet<ProviderEntity> Providers { get; set; } = null!;

    public DbSet<TagEntity> Tags { get; set; } = null!;

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlServer("Server=localhost;User ID=sa;Password=Hello12#;Database=EntityFrameworkMigration;trustServerCertificate=True;");
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        new ProductEntityConfig().Configure(modelBuilder.Entity<ProductEntity>());
        new ProductDetailEntityConfig().Configure(modelBuilder.Entity<ProductDetailEntity>());
        new ProviderEntityConfig().Configure(modelBuilder.Entity<ProviderEntity>());
        new TagEntityConfig().Configure(modelBuilder.Entity<TagEntity>());
    }
}