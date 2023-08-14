using Microsoft.EntityFrameworkCore;
using WebApi.Data.Repository;

namespace WebApi.Data;

public interface IUnitOfWork
{
    Task<int> Commit();
    void Rollback();
}

public sealed class UnitOfWork : IUnitOfWork, IDisposable
{
    private readonly ApplicationDbContext _context;
    private IProductRepository _productRepository = null!;


    public UnitOfWork(ApplicationDbContext context)
    {
        _context = context;
    }

    public IProductRepository ProductRepository
    {
        get
        {
            if (_productRepository == null)
            {
                _productRepository = new ProductRepository(_context);
            }

            return _productRepository;
        }
    }

    public Task<int> Commit()
    {
        return _context.SaveChangesAsync();
    }

    public void Rollback()
    {
        foreach (var entry in _context.ChangeTracker.Entries())
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.State = EntityState.Detached;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }

    private bool _disposed;

    private void Dispose(bool disposing)
    {
        if (!_disposed)
        {
            if (disposing)
            {
                _context.Dispose();
            }
        }

        _disposed = true;
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(typeof(UnitOfWork));
    }
}