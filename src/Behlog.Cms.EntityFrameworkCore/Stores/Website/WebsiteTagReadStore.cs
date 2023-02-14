using Behlog.Cms.Domain;
using Behlog.Cms.Store;
using Microsoft.EntityFrameworkCore;

namespace Behlog.Cms.EntityFrameworkCore.Stores;


public class WebsiteTagReadStore : IWebsiteTagReadStore
{
    private readonly IBehlogEntityFrameworkDbContext _db;
    private readonly DbSet<WebsiteTag> _set;
    
    public WebsiteTagReadStore(IBehlogEntityFrameworkDbContext db)
    {
        _db = db ?? throw new ArgumentNullException(nameof(db));
        _set = _db.Set<WebsiteTag>();
    }

    public async Task<bool> AnyAsync(
        Guid websiteId, Guid tagId, Guid contentId, CancellationToken cancellationToken = default)
    {
        return await _set.AnyAsync(_ => _.WebsiteId == websiteId &&
                                        _.TagId == tagId && 
                                        _.ContentId == contentId, 
            cancellationToken).ConfigureAwait(false);
    }

    public async Task<IReadOnlyCollection<WebsiteTag>> FindAllAsync(
        Guid websiteId, CancellationToken cancellationToken = default)
    {
        return (await _set
            .Where(_ => _.WebsiteId == websiteId)
            .GroupBy(_ => _.TagId)
            .Select(_ => _.FirstOrDefault())
            .ToListAsync(cancellationToken).ConfigureAwait(false))!;
    }

    public async Task<WebsiteTag> FindAsync(long id)
    {
        return (await _set.FindAsync(id).ConfigureAwait(false))!;
    }

    public async Task<IReadOnlyCollection<WebsiteTag>> FindAllAsync(
        Guid websiteId, Guid langId, CancellationToken cancellationToken = default)
    {
        return (await _set
            .Where(_ => _.WebsiteId == websiteId)
            .Where(_ => _.LangId == langId)
            .GroupBy(_ => _.TagId)
            .Select(_ => _.FirstOrDefault())
            .ToListAsync(cancellationToken).ConfigureAwait(false))!;
    }

    public async Task<IReadOnlyCollection<WebsiteTag>> FindByContentTypeAsync(
        Guid websiteId, Guid contentTypeId, CancellationToken cancellationToken = default)
    {
        return (await _set
            .Where(_ => _.WebsiteId == websiteId)
            .Where(_ => _.ContentTypeId == contentTypeId)
            .GroupBy(_ => _.TagId)
            .Select(_ => _.FirstOrDefault())
            .ToListAsync(cancellationToken).ConfigureAwait(false))!;
    }
}