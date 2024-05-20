using Microsoft.EntityFrameworkCore;
using Z.EntityFramework.Plus;

namespace Application.Libraries;
public static class QueryExcution<TEntity> where TEntity : class
{
    public static Task<List<TEntity>> ToListAsync(IQueryable<TEntity> query)
    {
        return query.ToListAsync();
    }
    public static Task<TEntity> ToSingleOrDefaultAsync(IQueryable<TEntity> query)
    {
        return query.SingleOrDefaultAsync();
    }
    public static Task<int> CountAsync(IQueryable<TEntity> query)
    {
        return query.CountAsync();
    }
    public static async Task<IList<TEntity>> ToListFromCacheAsync(IQueryable<TEntity> query)
    {
        var temp = await query.FromCacheAsync();
        return temp.ToList();
    }
    public static async Task<TEntity> ToSingleOrDefaultFromCacheAsync(IQueryable<TEntity> query)
    {
        var temp = await query.FromCacheAsync();
        return temp.SingleOrDefault();
    }
}
