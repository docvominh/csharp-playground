namespace EntityFrameWork.Database.Entity;

public class ProviderEntity
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Address { get; set; }

    public ICollection<ProductEntity> Products { get; set; }
}