using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore.Storage;
using Sample_CleanArchitecture_CQRS.Domain.Common.Interfaces;

namespace Sample_CleanArchitecture_CQRS.Infrastructure.Data;
public class UnitOfWork : IUnitOfWork, IDisposable
{

    private readonly AppDbContext dbContext;
    private bool disposed = false;
    private IDbContextTransaction? _transaction;




    public UnitOfWork(AppDbContext db)
    {
        dbContext = db;
    }
   
    public int SaveChanges(bool acceptAllChangesOnSuccess)
    {
        return dbContext.SaveChanges(acceptAllChangesOnSuccess);
    }

    public int SaveChanges()
    {
        return dbContext.SaveChanges();
    }

    public async Task RollbackTransactionAsync()
    {
        await _transaction?.RollbackAsync()!;
        await _transaction.DisposeAsync();
    }
    public async Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
    {
        return await dbContext.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
    }

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await dbContext.SaveChangesAsync(cancellationToken);
    }
    protected virtual void Dispose(bool disposing)
    {
        if (!this.disposed)
        {
            if (disposing)
            {
                dbContext.Dispose();
            }
        }
        this.disposed = true;
    }
    public void Dispose()
    {
        Dispose(true);
        _transaction?.Dispose();
        GC.SuppressFinalize(this);
    }

    public async Task BeginTransaction()
    {
        _transaction = await dbContext.Database.BeginTransactionAsync();
    }

    public async Task<int> CommitTransaction()
    {
        int result = 0;
        try
        {
            result = await dbContext.SaveChangesAsync();
            await _transaction?.CommitAsync()!;
        }
        catch
        {
            await _transaction?.RollbackAsync()!;
            throw;
        }
        finally
        {
            _transaction?.Dispose();
        }

        return result;
    }

    public async Task RollbackTransaction()
    {
        await _transaction?.RollbackAsync()!;
        _transaction.Dispose();
    }
}