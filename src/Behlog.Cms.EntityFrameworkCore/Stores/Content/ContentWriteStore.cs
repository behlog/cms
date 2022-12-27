using Behlog.Cms.Domain;
using Behlog.Core;

namespace Behlog.Cms.EntityFrameworkCore.Stores;

public class ContentWriteStore : BehlogEntityFrameworkCoreWriteStore<Content, Guid>, IContentWriteStore
{
    public ContentWriteStore(IBehlogEntityFrameworkDbContext db, IBehlogMediator mediator) 
        : base(db, mediator)
    {
    }
}