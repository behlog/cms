using Behlog.Core;
using Microsoft.EntityFrameworkCore;

namespace Behlog.Cms.EntityFrameworkCore;


public interface IBehlogEntityFrameworkDbContext : IBehlogDbContext
{

    DbSet<TEntity> Set<TEntity>() where TEntity : class;
    
    void AddRange<TEntity>(IEnumerable<TEntity> entities) where TEntity : class;

    Task AddRangeAsync<TEntity>(
        IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
        where TEntity : class;
    
    void RemoveRange<TEntity>(IEnumerable<TEntity> entities) where TEntity : class;

    Task RemoveRangeAsync<TEntity>(
        IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
        where TEntity : class;
    
    void MarkAsChanged<TEntity>(TEntity entity) where TEntity : class;

    void MarkAsChanged<TEntity>(IEnumerable<TEntity> entities) where TEntity : class;
}