using Behlog.Cms.Domain;
using Behlog.Cms.Store;
using Microsoft.EntityFrameworkCore;

namespace Behlog.Cms.EntityFrameworkCore.Stores;

public class WebsiteReadStore : BehlogReadStore<Website, Guid>,
    IWebsiteReadStore
{
    private readonly DbSet<Website> _websites;

    public WebsiteReadStore(IBehlogEntityFrameworkDbContext db) : base(db)
    {
        _websites = db.Set<Website>();
    }

    public async Task<Website?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _websites.Include(_ => _.Meta)
            .FirstOrDefaultAsync(_ => _.Id == id, cancellationToken: cancellationToken);
    }
}