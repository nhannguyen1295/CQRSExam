using System.Linq.Expressions;
using CQRSExam.Context;
using Microsoft.EntityFrameworkCore;

namespace CQRSExam.Repositories;

public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity:class
{
    internal ApplicationContext _context;
    internal DbSet<TEntity> _dbSet;

    public GenericRepository(ApplicationContext context)
    {
        _context = context;
        _dbSet = context.Set<TEntity>();
    }

    public virtual IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>>? filter = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null, string includeProperties = "")
    {
        IQueryable<TEntity> query = _dbSet;
        if (filter != null) query = query.Where(filter);

        query = includeProperties.Split(new[] {','}, StringSplitOptions.RemoveEmptyEntries).Aggregate(query, (current, property) => current.Include(property));

        return orderBy != null ? orderBy(query).ToList() : query.ToList();
    }

    public virtual TEntity? GetById(object id)
    {
        return _dbSet.Find(id);
    }

    public virtual void Insert(TEntity entity) => _dbSet.Add(entity);

    public virtual void Delete(TEntity entityToDelete)
    {
        if (_context.Entry(entityToDelete).State == EntityState.Detached) _dbSet.Attach(entityToDelete);
        _dbSet.Remove(entityToDelete);
    }

    public virtual void Update(TEntity entityToUpdate)
    {
        _dbSet.Attach(entityToUpdate);
        _context.Entry(entityToUpdate).State = EntityState.Modified;
    }
}