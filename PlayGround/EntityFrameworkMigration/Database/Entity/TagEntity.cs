using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EntityFrameworkMigration.Database.Entity;

public class TagEntity
{
    public int Id { get; set; }
    public required string Name { get; init; }
    public ICollection<ProductEntity> Products { get; set; }
}

public class TagEntityConfig : IEntityTypeConfiguration<TagEntity>
{
    public void Configure(EntityTypeBuilder<TagEntity> builder)
    {
        builder.ToTable("Tags");
        builder.Property(e => e.Id).HasColumnType("int").ValueGeneratedOnAdd();
        builder.Property(e => e.Name).HasColumnType("nvarchar(255)");

        builder.HasKey(e => new { e.Id }).HasName("PK_Tag");
    }
}