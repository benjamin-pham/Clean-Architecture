using Domain.Abstractions;
using Microsoft.EntityFrameworkCore.Storage;

namespace Persistence;

public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _dbContext;
    public UnitOfWork(ApplicationDbContext dbContext) => _dbContext = dbContext;
    private IDbContextTransaction _transaction;
    public async Task BeginTransactionAsync(CancellationToken cancellationToken = default)
    {
        _transaction = await _dbContext.Database.BeginTransactionAsync(cancellationToken);
    }
    public async Task CommitAsync(CancellationToken cancellationToken = default)
    {
        await _transaction?.CommitAsync(cancellationToken);
    }
    public async Task RollBackAsync(CancellationToken cancellationToken = default)
    {
        await _transaction?.RollbackAsync(cancellationToken);
    }
    public Task SaveAsync(CancellationToken cancellationToken = default) => _dbContext.SaveChangesAsync(cancellationToken);

    async ValueTask IAsyncDisposable.DisposeAsync()
    {
        await _dbContext.DisposeAsync();
    }
}
