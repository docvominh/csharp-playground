namespace WebApi.Models;

public class Product
{
    public int? Id { get; set; }
    public string? Name { get; set; }
    public string? Category { get; set; }
    public decimal? Price { get; set; }
    public string? Manufacture { get; set; }
    public ProductDetail? ProductDetail { get; set; }
    public List<Provider>? Providers { get; set; }
}