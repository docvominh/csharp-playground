namespace EntityFrameWork.Database.Entity;

public class ProductEntity
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Category { get; set; }
    public decimal? Price { get; set; }
    public string? Manufacture { get; set; }
    public virtual ProductDetailEntity? ProductDetail { get; set; }
    public virtual ICollection<ProductProviderEntity> ProductProviders { get; set; }
}