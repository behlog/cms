using Behlog.Cms.Domain;
using Behlog.Cms.Store;

namespace Behlog.Cms.EntityFrameworkCore.Stores;

public class WebsiteWriteStore : BehlogWriteStore<Website, Guid>,
    IWebsiteWriteStore
{
    
    public WebsiteWriteStore(IBehlogEntityFrameworkDbContext db) : base(db)
    {
    }
}