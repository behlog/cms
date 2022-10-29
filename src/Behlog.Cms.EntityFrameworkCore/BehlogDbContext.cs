using Behlog.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace Behlog.Cms.EntityFrameworkCore;

public class BehlogDbContext : DbContext, IBehlogDbContext
{
    private IDbContextTransaction _transaction;

    public BehlogDbContext(DbContextOptions options): base(options)
    {
        
    }
    
    public void Dispose()
    {
        throw new NotImplementedException();
    }

    public void BeginTransaction()
    {
        throw new NotImplementedException();
    }

    public void RollbackTransaction()
    {
        throw new NotImplementedException();
    }

    public void CommitTransaction()
    {
        throw new NotImplementedException();
    }

    public int SaveChanges()
    {
        throw new NotImplementedException();
    }

    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        throw new NotImplementedException();
    }

    public void ExecuteRawCommand(string query, params object[] parameters)
    {
        throw new NotImplementedException();
    }

    public bool EnsureCreated()
    {
        throw new NotImplementedException();
    }

    public void MigrateDb()
    {
        throw new NotImplementedException();
    }

    public Task MigrateDbAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        throw new NotImplementedException();
    }
}