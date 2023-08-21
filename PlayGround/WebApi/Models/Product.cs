using System.ComponentModel.DataAnnotations;
using WebApi.Attributes;

namespace WebApi.Models;

public class Product
{
    public int Id { get; set; }

    [Required(AllowEmptyStrings = false)]
    [Display(Name = "Product Name")]
    [StringLength(50, ErrorMessage = "{0} max length is {1}")]
    public string? Name { get; set; }

    public string? Category { get; set; }

    [Required] 
    [Range(10, 1500)] 
    public decimal? Price { get; set; }

    [ManufactureValidate(new[] { "Apple", "Dell" })]
    public string? Manufacture { get; set; }

    public ProductDetail? ProductDetail { get; set; }
    public List<Provider>? Providers { get; set; }
}