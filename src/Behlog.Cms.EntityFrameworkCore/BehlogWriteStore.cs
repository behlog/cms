using Behlog.Core;
using Behlog.Core.Domain;
using Behlog.Extensions;
using Microsoft.EntityFrameworkCore;

namespace Behlog.Cms.EntityFrameworkCore;

public class BehlogWriteStore<TEntity, TId> : IBehlogWriteStore<TEntity, TId> where TEntity : AggregateRoot<TId>
{
    private readonly IBehlogEntityFrameworkDbContext _db;
    private readonly DbSet<TEntity> _set;


    public BehlogWriteStore(IBehlogEntityFrameworkDbContext db)
    {
        _db = db ?? throw new ArgumentNullException(nameof(db));
        _set = _db.Set<TEntity>();
    }
 
    public async Task AddAsync(TEntity entity, CancellationToken cancellationToken = new CancellationToken())
    {
        entity.ThrowExceptionIfArgumentIsNull(nameof(entity));
        await _set.AddAsync(entity, cancellationToken).ConfigureAwait(false);
        await _db.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
    }

    public void MarkForAdd(TEntity entity)
    {
        entity.ThrowExceptionIfArgumentIsNull(nameof(entity));
        _set.Add(entity);
    }

    public async Task UpdateAsync(TEntity entity, CancellationToken cancellationToken = new CancellationToken())
    {
        entity.ThrowExceptionIfArgumentIsNull(nameof(entity));
        _set.Update(entity);
        await _db.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
    }

    public void MarkForUpdate(TEntity entity)
    {
        entity.ThrowExceptionIfArgumentIsNull(nameof(entity));
        _set.Update(entity);
    }

    public async Task DeleteAsync(TEntity entity, CancellationToken cancellationToken = new CancellationToken())
    {
        entity.ThrowExceptionIfArgumentIsNull(nameof(entity));
        _set.Remove(entity);
        await _db.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
    }

    public void MarkForDelete(TEntity entity)
    {
        entity.ThrowExceptionIfArgumentIsNull(nameof(entity));
        _set.Remove(entity);
    }

    public async Task SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        await _db.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
    }
}