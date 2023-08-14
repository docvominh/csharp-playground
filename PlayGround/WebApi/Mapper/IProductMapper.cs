using Riok.Mapperly.Abstractions;
using WebApi.Data.Entity;
using WebApi.Models;

namespace WebApi.Mapper;

public interface IProductMapper
{
    public Product ToModel(ProductEntity entity);
    public ProductEntity ToEntity(Product entity);
}

[Mapper]
partial class ProductMapper : IProductMapper
{
    public partial Product ToModel(ProductEntity entity);
    public partial ProductEntity ToEntity(Product entity);
}