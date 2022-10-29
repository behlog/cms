using Behlog.Core;
using Microsoft.EntityFrameworkCore;

namespace Behlog.Cms.EntityFrameworkCore;

public interface IBehlogEntityFrameworkDbContext : IBehlogDbContext
{

    DbSet<TEntity> Set<TEntity>() where TEntity : class;
    
    void AddRange<TEntity>(IEnumerable<TEntity> entities) where TEntity : class;
    
    void RemoveRange<TEntity>(IEnumerable<TEntity> entities) where TEntity : class;
    
    void MarkAsChanged<TEntity>(TEntity entity) where TEntity : class;
    
}