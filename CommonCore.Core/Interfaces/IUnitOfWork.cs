using System;
using System.Collections.Generic;
using System.Text;

namespace CommonCore.Core.Interfaces;

public interface IUnitOfWork
{
    Task<int> SaveChangesAsync(
       CancellationToken cancellationToken = default);

    Task BeginTransactionAsync();

    Task CommitTransactionAsync();

    Task RollbackTransactionAsync();
}
