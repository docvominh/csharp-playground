using EfCoreReplica.Data.Entity;
using Microsoft.EntityFrameworkCore;

namespace EfCoreReplica.Data;

public class ReadDbContext : DbContext
{
    public DbSet<Product> Products { get; init; }

    public ReadDbContext()
    {
    }

    public ReadDbContext(DbContextOptions<ReadDbContext> options) : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseNpgsql();
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Product>(
            e => { e.ToTable("Products"); }
        );
        base.OnModelCreating(modelBuilder);
    }
}
