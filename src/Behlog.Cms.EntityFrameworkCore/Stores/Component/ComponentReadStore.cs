using Behlog.Cms.Domain;
using Microsoft.EntityFrameworkCore;

namespace Behlog.Cms.EntityFrameworkCore.Stores;


public class ComponentReadStore : BehlogReadStore<Component, Guid>, IComponentReadStore
{
    
    public ComponentReadStore(IBehlogEntityFrameworkDbContext db) : base(db)
    {
    }

    /// <inheritdoc /> 
    public async Task<Component?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _set
            .Include(_=> _.Language)
            .Include(_=> _.Meta)
            .Include(_=> _.Files)
            .FirstOrDefaultAsync(_ => _.Id == id, cancellationToken)
            .ConfigureAwait(false);
    }

    /// <inheritdoc />
    public async Task<IReadOnlyCollection<Component>> GetByComponentTypeAsync(
        string componentType, CancellationToken cancellationToken = default)
    {
        return await _set
            .Include(_ => _.Language)
            .Include(_ => _.Meta)
            .Include(_ => _.Files)
            .Where(_ => _.ComponentType.ToUpper() == componentType.ToUpper())
            .ToListAsync(cancellationToken).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public async Task<IReadOnlyCollection<Component>> GetByWebsiteIdAsync(
        Guid websiteId, CancellationToken cancellationToken = default)
    {
        return await _set
            .Include(_ => _.Language)
            .Include(_ => _.Meta)
            .Include(_ => _.Files)
            .Where(_ => _.WebsiteId == websiteId)
            .ToListAsync(cancellationToken).ConfigureAwait(false);
    }
}