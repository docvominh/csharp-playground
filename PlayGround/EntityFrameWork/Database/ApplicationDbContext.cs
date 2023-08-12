using EntityFrameWork.Database.Configuration;
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

        new ProductEntityConfig().Configure(modelBuilder.Entity<ProductEntity>());
        new ProductDetailEntityConfig().Configure(modelBuilder.Entity<ProductDetailEntity>());
        new ProviderEntityConfig().Configure(modelBuilder.Entity<ProviderEntity>());
    }
}