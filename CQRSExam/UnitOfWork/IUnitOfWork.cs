using CQRSExam.Repositories;

namespace CQRSExam.UnitOfWork;

public interface IUnitOfWork<TEntity> where TEntity : class
{
    IGenericRepository<TEntity> Repository { get; }
    Task SaveAsync();
    void Dispose(bool disposing);
    void Dispose();
}