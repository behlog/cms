using Behlog.Cms.Domain;
using Behlog.Cms.Store;

namespace Behlog.Cms.EntityFrameworkCore.Stores;

public class TagWriteStore : BehlogWriteStore<Tag, Guid>,
    ITagWriteStore
{
    
    public TagWriteStore(IBehlogEntityFrameworkDbContext db) : base(db)
    {
    }
}