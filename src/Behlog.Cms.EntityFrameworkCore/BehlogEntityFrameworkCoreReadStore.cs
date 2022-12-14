using Behlog.Core;
using Behlog.Core.Domain;
using Microsoft.EntityFrameworkCore;

namespace Behlog.Cms.EntityFrameworkCore;

/// <summary>
/// EntityFrameworkCore implemetation of Behlog's ReadStore.
/// </summary>
/// <typeparam name="TEntity">Type of Entity.</typeparam>
/// <typeparam name="TId">Type of the Entity Identity.</typeparam>
public class BehlogEntityFrameworkCoreReadStore<TEntity, TId> 
    : IBehlogReadStore<TEntity, TId> where TEntity : AggregateRoot<TId>
{
    protected readonly IBehlogEntityFrameworkDbContext _db;
    protected readonly DbSet<TEntity> _set;

    public BehlogEntityFrameworkCoreReadStore(IBehlogEntityFrameworkDbContext db)
    {
        _db = db ?? throw new ArgumentNullException(nameof(db));
        _set = db.Set<TEntity>();
    }

    public async Task<TEntity> FindAsync(TId id, CancellationToken cancellationToken = new CancellationToken())
    {
        return (await _set.FindAsync(id, cancellationToken).ConfigureAwait(false))!;
    }

    public async Task<IReadOnlyCollection<TEntity>> FindAllAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        return await _set.ToListAsync(cancellationToken).ConfigureAwait(false);
    }
}