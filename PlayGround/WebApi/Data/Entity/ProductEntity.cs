using WebApi.Data.Repository.Base;

namespace WebApi.Data.Entity;

public class ProductEntity : BaseEntity
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Category { get; set; }
    public decimal? Price { get; set; }
    public string? Manufacture { get; set; }
    public virtual ProductDetailEntity? ProductDetail { get; set; }
    public virtual ICollection<ProviderEntity>? Providers { get; set; }

    public override object GetId()
    {
        return Id;
    }
}