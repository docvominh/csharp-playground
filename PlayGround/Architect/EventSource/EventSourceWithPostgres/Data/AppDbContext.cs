using EventSourceWithPostgres.Data.Entity;
using Microsoft.EntityFrameworkCore;

namespace EventSourceWithPostgres.Data;

public class AppDbContext : DbContext
{

    public DbSet<Event> Events;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(
            "Host=localhost; Port=5432; Database=event_source; Username=postgres; Password=Hello12#");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Event>(
            e =>
            {
                e.ToTable("events");
                e.HasKey(p => p.Id).HasName("pk_events");
                e.Property(p => p.Id).HasColumnType("SERIAL");
                e.Property(p => p.StreamId).HasColumnType("BIGINT");
                e.Property(p => p.Version).HasColumnType("BIGINT");
                e.Property(p => p.Data).HasColumnType("JSONB");

                e.HasIndex(p => new { p.StreamId, p.Version }).IsUnique();
            }
        );

        // builder.ToTable("Products");
        // builder.Property(e => e.Id).HasColumnType("int").ValueGeneratedOnAdd();
        // builder.Property(e => e.Name).IsRequired().HasMaxLength(255);
        // builder.Property(e => e.Category).HasMaxLength(100);
        // builder.Property(e => e.Price).HasColumnType("money");
        // builder.Property(e => e.Manufacture).HasMaxLength(255);
        base.OnModelCreating(modelBuilder);
    }
}
