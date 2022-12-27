using Behlog.Cms.Domain;
using Behlog.Cms.Store;

namespace Behlog.Cms.EntityFrameworkCore.Stores;

public class TagReadStore : BehlogEntityFrameworkCoreReadStore<Tag, Guid>,
    ITagReadStore
{
    
    public TagReadStore(IBehlogEntityFrameworkDbContext db) : base(db)
    {
    }
}