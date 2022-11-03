using Behlog.Cms.Domain;
using Behlog.Cms.Store;

namespace Behlog.Cms.EntityFrameworkCore.Stores;

public class ContentCategoryWriteStore : BehlogWriteStore<ContentCategory, Guid>, IContentCategoryWriteStore
{
    public ContentCategoryWriteStore(IBehlogEntityFrameworkDbContext db) 
        : base(db)
    {
    }
}