using Application.Libraries;
using Domain.Abstractions.BaseRepositories;
using Domain.Abstractions.Entities;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.DependencyInjection;
using System.Linq.Expressions;


namespace Persistence.BaseRepositories;

public abstract class BaseRepository<TEntity, TKey> : IBaseRepository<TEntity, TKey> where TEntity : BaseEntity<TKey>
{
    private static readonly List<Expression<Func<TEntity, object>>> updateExclude =
        [entity => entity.CreatedOn,
            entity => entity.CreatedBy,
            entity => entity.IsDeleted,
            entity => entity.DeletedOn,
            entity => entity.DeletedBy];

    private static readonly List<Expression<Func<TEntity, object>>> updateInclude =
        [entity => entity.UpdatedOn,
            entity => entity.UpdatedBy];

    private static readonly List<Expression<Func<TEntity, object>>> deleteInclude =
        [entity => entity.IsDeleted,
            entity => entity.DeletedOn,
            entity => entity.DeletedBy];

    protected readonly ApplicationDbContext _dbContext;
    protected IDateTime _datetime;

    protected readonly User UserContext = Application.Libraries.UserContext.Instance;

    public BaseRepository(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
    {
        _dbContext = dbContext;
        _datetime = serviceProvider.GetService<IDateTime>();
    }

    public IQueryable<TEntity> AsQueryable()
    {
        return _dbContext.Set<TEntity>().Where(entity => entity.IsDeleted == false).AsNoTracking();
    }

    public virtual Task<TEntity> FindByIdAsync(TKey id, Expression<Func<TEntity, TEntity>> includeProperties)
    {
        return _dbContext.Set<TEntity>().AsQueryable().Where(entity => entity.IsDeleted == false && entity.Id.Equals(id)).Select(includeProperties).SingleOrDefaultAsync();
    }

    public virtual Task<TEntity> FindByIdAsync(TKey id)
    {
        return _dbContext.Set<TEntity>().AsQueryable().Where(entity => entity.IsDeleted == false && entity.Id.Equals(id)).SingleOrDefaultAsync();
    }

    public virtual void Add(TEntity entity)
    {
        this.Add(new List<TEntity> { entity });
    }

    public virtual void Add(IList<TEntity> entities)
    {
        for (int i = 0; i < entities.Count; i++)
        {
            entities[i].CreatedBy = UserContext?.Id;

            entities[i].CreatedOn = _datetime.Now;
            entities[i].UpdatedBy = null;
            entities[i].UpdatedOn = null;
            entities[i].IsDeleted = false;
            entities[i].DeletedOn = null;
            entities[i].DeletedBy = null;
        }
        _dbContext.Set<TEntity>().AddRange(entities);
    }

    public virtual void Update(TEntity entity)
    {
        this.Update(new List<TEntity> { entity });
    }

    public virtual void Update(IList<TEntity> entities)
    {
        for (int i = 0; i < entities.Count; i++)
        {
            entities[i].UpdatedBy = UserContext?.Id;
            entities[i].UpdatedOn = _datetime.Now;

            EntityEntry<TEntity> dbEntry = _dbContext.Entry(entities[i]);
            dbEntry.State = EntityState.Modified;
            for (int j = 0; j < updateExclude.Count; j++)
            {
                dbEntry.Property(updateExclude[j]).IsModified = false;
            }
        }
    }

    public virtual void Update(TEntity entity, Expression<Func<TEntity, object>>[] includeProperties)
    {
        this.Update(new List<TEntity> { entity }, includeProperties);
    }

    public virtual void Update(IList<TEntity> entities, Expression<Func<TEntity, object>>[] includeProperties)
    {
        int entityCount = entities.Count;

        List<Expression<Func<TEntity, object>>> expressions = includeProperties.ToList();
        expressions.AddRange(updateInclude);
        for (int j = 0; j < updateExclude.Count; j++)
        {
            expressions.Remove(updateExclude[j]);
        }

        for (int i = 0; i < entityCount; i++)
        {
            entities[i].UpdatedBy = UserContext?.Id;
            entities[i].UpdatedOn = _datetime.Now;

            EntityEntry<TEntity> dbEntry = _dbContext.Entry(entities[i]);
            for (int j = 0; j < expressions.Count; j++)
            {
                dbEntry.Property(expressions[j]).IsModified = true;
            }
        }
    }

    public virtual void Delete(TEntity entity)
    {
        _dbContext.Set<TEntity>().Remove(entity);
    }

    public virtual void Delete(IList<TEntity> entities)
    {
        _dbContext.Set<TEntity>().RemoveRange(entities);
    }

    public virtual void SoftDelete(TEntity entity)
    {
        this.SoftDelete(new List<TEntity> { entity });
    }

    public virtual void SoftDelete(IList<TEntity> entities)
    {
        for (int i = 0; i < entities.Count; i++)
        {
            entities[i].IsDeleted = true;
            entities[i].DeletedOn = _datetime.Now;
            entities[i].DeletedBy = UserContext?.Id;

            EntityEntry<TEntity> dbEntry = _dbContext.Entry(entities[i]);

            for (int j = 0; j < deleteInclude.Count; j++)
            {
                dbEntry.Property(deleteInclude[j]).IsModified = true;
            }
        }
    }
}