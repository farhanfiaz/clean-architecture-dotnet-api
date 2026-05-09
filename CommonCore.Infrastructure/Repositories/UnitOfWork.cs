using CommonCore.Core.Interfaces;
using CommonCore.Infrastructure.Data;
using Microsoft.EntityFrameworkCore.Storage;

namespace CommonCore.Infrastructure.Repositories;

public class UnitOfWork(CommonCoreDbContext _dbContext, IDbContextTransaction? _transaction) : IUnitOfWork
{
    public async Task BeginTransactionAsync()
    {
        _transaction =
            await _dbContext.Database.BeginTransactionAsync();
    }

    public async Task CommitTransactionAsync()
    {
        await _dbContext.SaveChangesAsync();

        if (_transaction != null)
        {
            await _transaction.CommitAsync();
        }
    }

    public async Task RollbackTransactionAsync()
    {
        if (_transaction != null)
        {
            await _transaction.RollbackAsync();
        }
    }

    public async Task<int> SaveChangesAsync(
        CancellationToken cancellationToken = default)
    {
        return await _dbContext.SaveChangesAsync(cancellationToken);
    }
}
