using Behlog.Cms.Domain;
using Behlog.Cms.Store;
using Behlog.Core;

namespace Behlog.Cms.EntityFrameworkCore.Stores;

public class ContentCategoryWriteStore : BehlogWriteStore<ContentCategory, Guid>, IContentCategoryWriteStore
{
    public ContentCategoryWriteStore(IBehlogEntityFrameworkDbContext db, IBehlogMediator mediator) 
        : base(db, mediator)
    {
    }
}