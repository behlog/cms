using Behlog.Core;
using Behlog.Extensions;
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
        _transaction?.Dispose();
        base.Dispose();
    }

    public void BeginTransaction()
    {
        _transaction = Database.BeginTransaction();
    }

    public async Task BeginTransactionAsync(CancellationToken cancellationToken = default)
    {
        _transaction = await Database.BeginTransactionAsync(cancellationToken).ConfigureAwait(false);
    }

    public void RollbackTransaction()
    {
        if (_transaction is null)
        {
            throw new NullReferenceException(
                "The transaction is null. You have to call 'BeginTransaction' method first.");
        }
        _transaction.Rollback();
    }

    public async Task RollbackTransactionAsync(CancellationToken cancellationToken = default)
    {
        if (_transaction is null)
        {
            throw new NullReferenceException(
                "The transaction is null. You have to call 'BeginTransactionAsync' method first.");
        }

        await _transaction.RollbackAsync(cancellationToken);
    }

    public void CommitTransaction()
    {
        if (_transaction is null)
        {
            throw new NullReferenceException(
                "The transaction is null. You have to call 'BeginTransaction' method first.");
        }
        
        _transaction.Commit();
    }

    public async Task CommitTransactionAsync(CancellationToken cancellationToken = default)
    {
        if (_transaction is null)
        {
            throw new NullReferenceException(
                "The transaction is null. You have to call 'BeginTransactionAsync' method first.");
        }

        await _transaction.CommitAsync(cancellationToken).ConfigureAwait(false);
    }

    public int SaveChanges()
    {
        ChangeTracker.DetectChanges();
        ChangeTracker.AutoDetectChangesEnabled = false;
        var result = base.SaveChanges();
        ChangeTracker.AutoDetectChangesEnabled = true;
        return result;
    }

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        ChangeTracker.DetectChanges();
        ChangeTracker.AutoDetectChangesEnabled = false;
        var result = await base.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
        ChangeTracker.AutoDetectChangesEnabled = true;
        return result;
    }

    public void ExecuteRawCommand(string query, params object[] parameters)
    {
        Database.ExecuteSqlRaw(query, parameters);
    }

    public async Task ExecuteRawCommandAsync(
        string query, object[] parameters, CancellationToken cancellationToken = default)
    {
        await Database.ExecuteSqlRawAsync(query, parameters, cancellationToken).ConfigureAwait(false);
    }

    public bool EnsureCreated()
    {
        return base.Database.EnsureCreated();
    }

    public async Task<bool> EnsureCreatedAsync(CancellationToken cancellationToken = default)
    {
        return await base.Database.EnsureCreatedAsync(cancellationToken).ConfigureAwait(false);
    }

    public void MigrateDb()
    {
        Database.Migrate();
    }

    public async Task MigrateDbAsync(CancellationToken cancellationToken = default)
    {
        await Database.MigrateAsync(cancellationToken).ConfigureAwait(false);
    }
}