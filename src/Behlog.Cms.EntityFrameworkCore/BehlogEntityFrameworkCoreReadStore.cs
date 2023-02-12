using Behlog.Core;
using Behlog.Core.Contracts;
using Behlog.Core.Domain;
using Microsoft.EntityFrameworkCore;

namespace Behlog.Cms.EntityFrameworkCore;

/// <summary>
/// EntityFrameworkCore implementation of Behlog's ReadStore.
/// </summary>
/// <typeparam name="TEntity">Type of Entity.</typeparam>
/// <typeparam name="TId">Type of the Entity Identity.</typeparam>
public class BehlogEntityFrameworkCoreReadStore<TEntity, TId> 
    : IBehlogReadStore<TEntity, TId> where TEntity : BehlogEntity<TId>
{
    protected readonly IBehlogEntityFrameworkDbContext _db;
    protected readonly DbSet<TEntity> _set;

    public BehlogEntityFrameworkCoreReadStore(IBehlogEntityFrameworkDbContext db)
    {
        _db = db ?? throw new ArgumentNullException(nameof(db));
        _set = db.Set<TEntity>();
    }

    public async Task<TEntity> FindAsync(
        TId id, CancellationToken cancellationToken = default)
    {
        return (await _set.FindAsync(
            new object?[] { id }, cancellationToken: cancellationToken
            ).ConfigureAwait(false))!;
    }

    public async Task<IReadOnlyCollection<TEntity>> FindAllAsync(
        CancellationToken cancellationToken = default)
    {
        return await _set.ToListAsync(cancellationToken).ConfigureAwait(false);
    }
}