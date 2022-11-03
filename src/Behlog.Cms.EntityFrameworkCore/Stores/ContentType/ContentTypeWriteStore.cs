using Behlog.Cms.Store;
using Behlog.Core;

namespace Behlog.Cms.EntityFrameworkCore.Stores;

public class ContentTypeWriteStore : BehlogWriteStore<ContentType, Guid>, IContentTypeWriteStore
{
    public ContentTypeWriteStore(IBehlogEntityFrameworkDbContext db) 
        : base(db)
    {
    }
}