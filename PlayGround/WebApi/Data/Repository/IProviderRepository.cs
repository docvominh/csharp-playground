using WebApi.Data.Entity;
using WebApi.Data.Repository.Base;

namespace WebApi.Data.Repository;

public interface IProviderRepository : ICrudRepository<ProviderEntity>, IRepository<ProductEntity>
{
}