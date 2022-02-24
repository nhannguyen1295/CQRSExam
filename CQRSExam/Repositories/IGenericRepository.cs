using System.Linq.Expressions;

namespace CQRSExam.Repositories;

public interface IGenericRepository<TEntity> where TEntity : class
{
    Task<IEnumerable<TEntity>> GetAsync(Expression<Func<TEntity, bool>>? filter = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null, string includeProperties = "");

    Task<TEntity?> GetByIdAsync(object id);
    void Insert(TEntity entity);
    void Delete(TEntity entityToDelete);
    void Update(TEntity entityToUpdate);
}