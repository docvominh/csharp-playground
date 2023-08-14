using Microsoft.EntityFrameworkCore;
using WebApi.Data.Entity;
using WebApi.Data.Repository.Base;

namespace WebApi.Data.Repository;

public interface IProductRepository : ICrudRepository<ProductEntity>, IRepository<ProductEntity>
{
}

internal class ProductRepository : CrudRepository<ProductEntity>, IProductRepository
{
    public ProductRepository(DbContext context) : base(context)
    {
    }
}