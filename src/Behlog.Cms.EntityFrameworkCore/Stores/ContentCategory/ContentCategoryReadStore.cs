using Behlog.Cms.Domain;
using Behlog.Cms.Store;

namespace Behlog.Cms.EntityFrameworkCore.Stores;

public class ContentCategoryReadStore : BehlogReadStore<ContentCategory, Guid>, IContentCategoryReadStore
{
    public ContentCategoryReadStore(IBehlogEntityFrameworkDbContext db) 
        : base(db)
    {
    }
}