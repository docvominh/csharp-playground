using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApi.Data.Entity;

namespace WebApi.Data.Configuration;

public class ProviderEntityConfig : IEntityTypeConfiguration<ProviderEntity>
{
    public void Configure(EntityTypeBuilder<ProviderEntity> builder)
    {
        builder.ToTable("Providers");
        builder.Property(e => e.Id).ValueGeneratedOnAdd();
        builder.Property(e => e.Name).HasColumnType("nvarchar(255)");
        builder.Property(e => e.Address).HasColumnType("nvarchar(255)");

        builder.HasKey(e => new { e.Id }).HasName("PK_Providers");
    }
}