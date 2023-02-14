using Behlog.Cms.Domain;
using Behlog.Cms.Store;
using Behlog.Extensions;
using Microsoft.EntityFrameworkCore;

namespace Behlog.Cms.EntityFrameworkCore.Stores;

public class WebsiteTagWriteStore : IWebsiteTagWriteStore
{
    private readonly IBehlogEntityFrameworkDbContext _db;
    private readonly DbSet<WebsiteTag> _set;

    public WebsiteTagWriteStore(IBehlogEntityFrameworkDbContext db)
    {
        _db = db ?? throw new ArgumentNullException(nameof(db));
        _set = db.Set<WebsiteTag>();
    }

    public async Task AddAsync(
        WebsiteTag websiteTag, CancellationToken cancellationToken = default)
    {
        websiteTag.ThrowExceptionIfArgumentIsNull(nameof(websiteTag));
        await _set.AddAsync(websiteTag, cancellationToken).ConfigureAwait(false);
        await _db.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
    }

    public async Task UpdateAsync(
        WebsiteTag websiteTag, CancellationToken cancellationToken = default)
    {
        websiteTag.ThrowExceptionIfArgumentIsNull(nameof(websiteTag));
        _set.Update(websiteTag);
        await _db.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
    }

    public async Task DeleteAsync(
        WebsiteTag websiteTag, CancellationToken cancellationToken = default)
    {
        websiteTag.ThrowExceptionIfArgumentIsNull(nameof(websiteTag));
        _set.Remove(websiteTag);
        await _db.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
    }
}