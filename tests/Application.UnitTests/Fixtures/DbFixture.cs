using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

using Sample_CleanArchitecture_CQRS.Infrastructure.Data;

namespace Application.UnitTests.Fixtures;

public class DbFixture : IDisposable
{
    public AppDbContext DbContext { get; private set; }
    private bool _disposed;


    public DbFixture()
    {

        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        DbContext = new AppDbContext(options);
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (_disposed)
        {
            return;
        }

        if (disposing)
        {
            DbContext.Dispose();
        }

        _disposed = true;
    }

}

