using WebApi.Data.Repository.Base;

namespace WebApi.Data.Entity;

public class ProviderEntity : BaseEntity
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Address { get; set; }

    public ICollection<ProductEntity>? Products { get; set; }

    public override object GetId()
    {
        return Id;
    }
}