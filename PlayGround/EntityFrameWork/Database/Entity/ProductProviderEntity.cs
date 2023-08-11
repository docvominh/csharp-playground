namespace EntityFrameWork.Database.Entity;

public class ProductProviderEntity
{
    public int ProductId { get; set; }
    public int ProviderId { get; set; }

    public ProductEntity Product { get; set; }
    public ProviderEntity Provider { get; set; }
}