using Behlog.Cms.Store;
using Behlog.Core;

namespace Behlog.Cms.EntityFrameworkCore.Stores;

public class ContentTypeReadStore : BehlogReadStore<ContentType, Guid>, IContentTypeReadStore
{
    public ContentTypeReadStore(IBehlogEntityFrameworkDbContext db) 
        : base(db)
    {
    }
}