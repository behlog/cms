using Behlog.Cms.Domain;
using Behlog.Cms.Store;
using Behlog.Core;

namespace Behlog.Cms.EntityFrameworkCore.Stores;

public class WebsiteWriteStore : BehlogEntityFrameworkCoreWriteStore<Website, Guid>,
    IWebsiteWriteStore
{
    
    public WebsiteWriteStore(IBehlogEntityFrameworkDbContext db, IBehlogMediator mediator) 
        : base(db, mediator)
    {
    }
}