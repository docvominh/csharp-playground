using System.ComponentModel.DataAnnotations.Schema;
using EntityFrameWork.Database.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EntityFrameWork.Database.Configuration;

public class ProviderEntityConfig : IEntityTypeConfiguration<ProviderEntity>
{
    public void Configure(EntityTypeBuilder<ProviderEntity> builder)
    {
        builder.ToTable("Providers");
        builder.Property(e => e.Id).HasColumnType("int").ValueGeneratedOnAdd();
        builder.Property(e => e.Name).HasColumnType("nvarchar(255)");
        builder.Property(e => e.Address).HasColumnType("nvarchar(255)");

        builder.HasKey(e => new { e.Id }).HasName("PK_Providers");
    }
}