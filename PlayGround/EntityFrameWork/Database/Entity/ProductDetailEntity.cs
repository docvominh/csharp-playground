namespace EntityFrameWork.Database.Entity;

public class ProductDetailEntity
{
    public int ProductId { get; set; }
    public string? Comment { get; set; }
    public virtual ProductEntity Product { get; set; } = null!;
}