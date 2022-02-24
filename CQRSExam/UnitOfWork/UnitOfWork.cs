using CQRSExam.Context;
using CQRSExam.Repositories;

namespace CQRSExam.UnitOfWork;

public class UnitOfWork<TEntity> : IUnitOfWork<TEntity> where TEntity:class
{
    private readonly ApplicationContext _context;
    private IGenericRepository<TEntity>? _repository;

    public UnitOfWork(ApplicationContext context) => _context = context;

    public IGenericRepository<TEntity> Repository => _repository ??= new GenericRepository<TEntity>(_context);

    public Task SaveAsync() => _context.SaveChanges();

    private bool _disposed;

    public virtual void Dispose(bool disposing)
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
        Console.WriteLine("Dispose called");
        Dispose(true);
        GC.SuppressFinalize(this);
    }
    
}