using Behlog.Core;
using Behlog.Extensions;
using Microsoft.EntityFrameworkCore;

namespace Behlog.Cms.EntityFrameworkCore;

public class BehlogRepository<TEntity, TId> : IBehlogRepository<TEntity, TId> where TEntity : class
{

    private readonly IBehlogEntityFrameworkDbContext _db;
    private readonly DbSet<TEntity> _set;

    public BehlogRepository(IBehlogEntityFrameworkDbContext db)
    {
        db.ThrowExceptionIfArgumentIsNull(nameof(db));
        _db = db;
        _set = _db.Set<TEntity>();
    }

    public async Task AddAsync(TEntity entity, CancellationToken cancellationToken = default)
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

    public async Task UpdateAsync(TEntity entity, CancellationToken cancellationToken = default)
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

    public async Task DeleteAsync(TEntity entity, CancellationToken cancellationToken = default)
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

    public async Task<TEntity> FindAsync(TId id)
    {
        return (await _set.FindAsync(id).ConfigureAwait(false))!;
    }

    public async Task<IReadOnlyCollection<TEntity>> FindAllAsync(CancellationToken cancellationToken = default)
    {
        return await _set.ToListAsync(cancellationToken);
    }

    public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        await _db.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
    }
}