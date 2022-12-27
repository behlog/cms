using Behlog.Core;
using Behlog.Extensions;
using Behlog.Core.Domain;
using Microsoft.EntityFrameworkCore;

namespace Behlog.Cms.EntityFrameworkCore;


public class BehlogEntityFrameworkCoreWriteStore<TEntity, TId> 
    : IBehlogWriteStore<TEntity, TId> where TEntity : AggregateRoot<TId>
{
    private readonly IBehlogEntityFrameworkDbContext _db;
    private readonly DbSet<TEntity> _set;
    private readonly IBehlogMediator _mediator;
    private ICollection<IBehlogEvent> _stagedEvents;

    public BehlogEntityFrameworkCoreWriteStore(
        IBehlogEntityFrameworkDbContext db, IBehlogMediator mediator)
    {
        _db = db ?? throw new ArgumentNullException(nameof(db));
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        _set = _db.Set<TEntity>();
        _stagedEvents = new List<IBehlogEvent>();
    }
 
    public async Task AddAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        entity.ThrowExceptionIfArgumentIsNull(nameof(entity));
        await _set.AddAsync(entity, cancellationToken).ConfigureAwait(false);
        await _db.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
        await PublishEventsAsync(_mediator, entity, cancellationToken);
    }

    public void MarkForAdd(TEntity entity)
    {
        entity.ThrowExceptionIfArgumentIsNull(nameof(entity));
        _set.Add(entity);
        StageEvents(entity);
    }

    public async Task UpdateAsync(TEntity entity, CancellationToken cancellationToken = new CancellationToken())
    {
        entity.ThrowExceptionIfArgumentIsNull(nameof(entity));
        _set.Update(entity);
        await _db.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
        await PublishEventsAsync(_mediator, entity, cancellationToken);
    }

    public void MarkForUpdate(TEntity entity)
    {
        entity.ThrowExceptionIfArgumentIsNull(nameof(entity));
        _set.Update(entity);
        StageEvents(entity);
    }

    public async Task DeleteAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        entity.ThrowExceptionIfArgumentIsNull(nameof(entity));
        _set.Remove(entity);
        await _db.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
        await PublishEventsAsync(_mediator, entity, cancellationToken);

    }

    public void MarkForDelete(TEntity entity)
    {
        entity.ThrowExceptionIfArgumentIsNull(nameof(entity));
        _set.Remove(entity);
        StageEvents(entity);
    }

    public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        await _db.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
        await PublishEventsAsync(cancellationToken);
    }


    #region Private methods

    private void StageEvents(TEntity entity)
    {
        if(entity is null) return;
        var events = entity.GetAllEvents();
        
        if(events is null || !events.Any())
            return;
        
        foreach(var e in events)
            _stagedEvents.Add(e);
    }

    private async Task PublishEventsAsync(CancellationToken cancellationToken = default)
    {
        if(!_stagedEvents.Any()) return;

        await PublishEventsAsync(_mediator, _stagedEvents.ToList(), cancellationToken);
    }
    
    private static async Task PublishEventsAsync(
        IBehlogMediator mediator, TEntity entity, CancellationToken cancellationToken = default)
    {
        var events = entity.GetAllEvents();

        await PublishEventsAsync(mediator, events, cancellationToken);
    }

    private static async Task PublishEventsAsync(
        IBehlogMediator mediator, IReadOnlyCollection<IBehlogEvent> events, 
        CancellationToken cancellationToken = default)
    {
        if(events is null || !events.Any()) return;

        await mediator.PublishAsync(events, cancellationToken).ConfigureAwait(false);
    }

    #endregion
}