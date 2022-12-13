using Behlog.Cms.Domain;
using Behlog.Cms.EntityFrameworkCore.Configurations;
using Behlog.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace Behlog.Cms.EntityFrameworkCore;


public class BehlogDbContext : DbContext, IBehlogEntityFrameworkDbContext
{
    private IDbContextTransaction _transaction;

    public BehlogDbContext(DbContextOptions options): base(options)
    {
    }
    
    public new void Dispose()
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

    public new int SaveChanges()
    {
        ChangeTracker.DetectChanges();
        ChangeTracker.AutoDetectChangesEnabled = false;
        var result = base.SaveChanges();
        ChangeTracker.AutoDetectChangesEnabled = true;
        return result;
    }

    public new async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
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

    public bool CheckDatabaseExist()
    {
        return Database.CanConnect();
    }

    public void AddRange<TEntity>(IEnumerable<TEntity> entities) where TEntity : class
    {
        Set<TEntity>().AddRange(entities);
    }

    public async Task AddRangeAsync<TEntity>(
        IEnumerable<TEntity> entities, CancellationToken cancellationToken = default) where TEntity : class
    {
        await Set<TEntity>().AddRangeAsync(entities, cancellationToken).ConfigureAwait(false);
    }

    public void RemoveRange<TEntity>(IEnumerable<TEntity> entities) where TEntity : class
    {
        Set<TEntity>().RemoveRange(entities);
    }

    public void MarkAsChanged<TEntity>(TEntity entity) where TEntity : class
    {
        Set<TEntity>().Update(entity);
    }

    public void MarkAsChanged<TEntity>(IEnumerable<TEntity> entities) where TEntity : class
    {
        Set<TEntity>().UpdateRange(entities);
    }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Ignore<CommentStatus>();
        modelBuilder.Ignore<ContentStatus>();
        modelBuilder.Ignore<EntityStatus>();
        modelBuilder.Ignore<FileUploadStatus>();
        modelBuilder.Ignore<WebsiteStatus>();
        modelBuilder.Ignore<ContentBodyType>();

        modelBuilder.AddWebsiteConfiguration();
        modelBuilder.AddLanguageConfiguration();
        modelBuilder.AddContentTypeConfiguration();
        modelBuilder.AddContentConfiguration();
        modelBuilder.AddContentCategoryConfiguration();
        modelBuilder.AddCommentConfiguration();
        modelBuilder.AddFileUploadConfiguration();
        modelBuilder.AddTagConfiguration();
        modelBuilder.AddComponentConfiguration();
        
        base.OnModelCreating(modelBuilder);
    }
}