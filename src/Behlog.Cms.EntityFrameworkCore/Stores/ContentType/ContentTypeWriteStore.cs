using Behlog.Core;
using Behlog.Cms.Store;
using Behlog.Cms.Domain;

namespace Behlog.Cms.EntityFrameworkCore.Stores;


public class ContentTypeWriteStore : BehlogEntityFrameworkCoreWriteStore<ContentType, Guid>, IContentTypeWriteStore
{
    public ContentTypeWriteStore(IBehlogEntityFrameworkDbContext db, IBehlogMediator mediator)
        : base(db, mediator)
    {
    }
}