using WebApi.Data.Repository.Base;

namespace WebApi.Data.Entity;

public class ProductDetailEntity : BaseEntity
{
    public int ProductId { get; set; }
    public string? Comment { get; set; }
    public virtual ProductEntity Product { get; set; } = null!;

    public override object GetId()
    {
        return ProductId;
    }
}