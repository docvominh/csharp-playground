using Microsoft.EntityFrameworkCore;

namespace WebApi.Data.Repository.Base;

public interface ICrudRepository<T> where T : BaseEntity
{
    Task<T?> FindById(object id);
    Task<List<T>> FindAll();
    T Save(T entity);
    T Update(T entity);
    IEnumerable<T> SaveAll(List<T> entities);
    void Delete(T entity);
    Task DeleteById(object id);
    Task DeleteAll();
    Task DeleteAllById(List<object> ids);
}

internal abstract class CrudRepository<T> : ICrudRepository<T> where T : BaseEntity
{
    private readonly DbSet<T> _dbSet;
    private readonly DbContext _context;

    protected CrudRepository(DbContext context)
    {
        _context = context;
        _dbSet = context.Set<T>();
    }

    public async Task<T?> FindById(object id)
    {
        return await _dbSet.FindAsync(id);
    }

    public async Task<List<T>> FindAll()
    {
        return await _dbSet.ToListAsync();
    }

    public T Save(T entity)
    {
        return _dbSet.Add(entity).Entity;
    }

    public T Update(T entity)
    {
        return _context.Update(entity).Entity;
    }

    public IEnumerable<T> SaveAll(List<T> entities)
    {
        _dbSet.AddRange(entities);
        return entities;
    }

    public void Delete(T entity)
    {
        _dbSet.Remove(entity);
    }

    public async Task DeleteById(object id)
    {
        await _dbSet.Where(x => x.GetId().Equals(id)).ExecuteDeleteAsync();
    }

    public async Task DeleteAll()
    {
        await _dbSet.ExecuteDeleteAsync();
    }

    public async Task DeleteAllById(List<object> ids)
    {
        await _dbSet.Where(x => ids.Contains(x.GetId())).ExecuteDeleteAsync();
    }
}