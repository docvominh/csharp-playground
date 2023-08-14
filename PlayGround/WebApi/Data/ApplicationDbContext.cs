using Microsoft.EntityFrameworkCore;
using WebApi.Data.Configuration;
using WebApi.Data.Entity;

namespace WebApi.Data;

public class ApplicationDbContext : DbContext
{
    public DbSet<ProductEntity> Products { get; set; } = null!;
    public DbSet<ProviderEntity> Providers { get; set; } = null!;

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        new ProductEntityConfig().Configure(modelBuilder.Entity<ProductEntity>());
        new ProductDetailEntityConfig().Configure(modelBuilder.Entity<ProductDetailEntity>());
        new ProviderEntityConfig().Configure(modelBuilder.Entity<ProviderEntity>());
    }
}