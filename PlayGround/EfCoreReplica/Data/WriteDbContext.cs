using EfCoreReplica.Data.Entity;
using Microsoft.EntityFrameworkCore;

namespace EfCoreReplica.Data;

public class WriteDbContext : DbContext
{
    public DbSet<Product> Products { get; init; }

    public WriteDbContext()
    {
    }

    public WriteDbContext(DbContextOptions<WriteDbContext> options) : base(options)
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
