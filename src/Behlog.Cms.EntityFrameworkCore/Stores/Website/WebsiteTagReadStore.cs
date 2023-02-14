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
    }

    public Task<bool> AnyAsync(Guid websiteId, Guid tagId, Guid contentId, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<IReadOnlyCollection<WebsiteTag>> FindAllAsync(Guid websiteId, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<WebsiteTag> FindAsync(long id)
    {
        throw new NotImplementedException();
    }

    public Task<IReadOnlyCollection<WebsiteTag>> FindAllAsync(Guid websiteId, Guid langId, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<IReadOnlyCollection<WebsiteTag>> FindByContentTypeAsync(Guid websiteId, Guid contentTypeId, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}