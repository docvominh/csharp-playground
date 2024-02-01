using BankAccountEventSource.Controller;
using BankAccountEventSource.Persistence.Entity;
using Microsoft.EntityFrameworkCore;

namespace BankAccountEventSource.Persistence;

public class AppDbContext : DbContext
{
    public DbSet<AccountEntity> Accounts { get; init; }

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
            optionsBuilder.UseNpgsql();
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AccountEntity>(
            e => { e.ToTable("accounts"); }
        );


        base.OnModelCreating(modelBuilder);
    }
}
