using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample_CleanArchitecture_CQRS.Domain.Common.Interfaces;
public interface IUnitOfWork
{
    int SaveChanges(bool acceptAllChangesOnSuccess);
    int SaveChanges();
    Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default);
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

    Task BeginTransaction();
    Task<int> CommitTransaction();
    Task RollbackTransaction();
}
