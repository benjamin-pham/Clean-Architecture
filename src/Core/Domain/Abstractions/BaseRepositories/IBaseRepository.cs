using Domain.Abstractions.Entities;
using System.Linq.Expressions;

namespace Domain.Abstractions.BaseRepositories;

public interface IBaseRepository<TEntity, TKey> where TEntity : BaseEntity<TKey>
{
    IQueryable<TEntity> AsQueryable();

    Task<TEntity> FindByIdAsync(TKey id, Expression<Func<TEntity, TEntity>> includeProperties);
    Task<TEntity> FindByIdAsync(TKey id);

    void Add(TEntity entity);
    void Add(IList<TEntity> entities);

    void Update(TEntity entity);
    void Update(IList<TEntity> entities);
    void Update(TEntity entity, params Expression<Func<TEntity, object>>[] includeProperties);
    void Update(IList<TEntity> entities, params Expression<Func<TEntity, object>>[] includeProperties);

    void Delete(TEntity entity);
    void Delete(IList<TEntity> entities);

    void SoftDelete(TEntity entity);
    void SoftDelete(IList<TEntity> entities);

}