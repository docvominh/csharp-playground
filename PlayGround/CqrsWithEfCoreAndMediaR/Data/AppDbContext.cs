using CqrsWithEfCoreAndMediaR.Data.Entity;
using Microsoft.EntityFrameworkCore;

namespace CqrsWithEfCoreAndMediaR.Data;

public class AppDbContext : DbContext
{
    public DbSet<Product> Products { get; init; }

    public AppDbContext()
    {
    }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlite();
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
