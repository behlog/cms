using Behlog.Cms.Domain;
using Microsoft.EntityFrameworkCore;

namespace Behlog.Cms.EntityFrameworkCore.Stores;

public class ContentReadStore : BehlogReadStore<Content, Guid>, IContentReadStore
{
    public ContentReadStore(IBehlogEntityFrameworkDbContext db) 
        : base(db)
    {
    }

    public async Task<Content?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _set
            .Include(_ => _.Categories)
            .Include(_ => _.Blocks)
            .Include(_ => _.Components)
            .Include(_ => _.Files)
            .Include(_ => _.Language)
            .Include(_ => _.Meta)
            .Include(_ => _.ContentType)
            .FirstOrDefaultAsync(_ => _.Id == id, cancellationToken).ConfigureAwait(false);
    }
}