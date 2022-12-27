using Behlog.Cms.Domain;
using Behlog.Cms.Store;
using Behlog.Core;

namespace Behlog.Cms.EntityFrameworkCore.Stores;

public class TagWriteStore : BehlogEntityFrameworkCoreWriteStore<Tag, Guid>,
    ITagWriteStore
{
    
    public TagWriteStore(IBehlogEntityFrameworkDbContext db, IBehlogMediator mediator) 
        : base(db, mediator)
    {
    }
}