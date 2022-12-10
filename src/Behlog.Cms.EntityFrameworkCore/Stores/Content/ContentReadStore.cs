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
    
    public async Task<int> CountLikesAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _db.Set<ContentLike>().CountAsync(
            _ => _.ContentId == id, cancellationToken).ConfigureAwait(false);
    }

    public async Task<Content?> GetBySlugAsync(
        Guid websiteId, string slug, CancellationToken cancellationToken = default)
    {
        return await _set
            .Include(_ => _.Categories)
            .Include(_ => _.Blocks)
            .Include(_ => _.Components)
            .Include(_ => _.Files)
            .Include(_ => _.Language)
            .Include(_ => _.Meta)
            .Include(_ => _.ContentType)
            .FirstOrDefaultAsync(_ => _.WebsiteId == websiteId &&
                                      _.Slug.ToUpper() == slug.ToUpper(), cancellationToken)
            .ConfigureAwait(false);
    }
}