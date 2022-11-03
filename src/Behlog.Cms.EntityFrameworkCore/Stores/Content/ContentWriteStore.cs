using Behlog.Cms.Domain;

namespace Behlog.Cms.EntityFrameworkCore.Stores;

public class ContentWriteStore : BehlogWriteStore<Content, Guid>, IContentWriteStore
{
    public ContentWriteStore(IBehlogEntityFrameworkDbContext db) 
        : base(db)
    {
    }
}