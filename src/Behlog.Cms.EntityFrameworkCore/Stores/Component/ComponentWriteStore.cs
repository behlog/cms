using Behlog.Core;
using Behlog.Cms.Domain;

namespace Behlog.Cms.EntityFrameworkCore.Stores;

public class ComponentWriteStore : BehlogEntityFrameworkCoreWriteStore<Component, Guid>,
    IComponentWriteStore
{
    
    public ComponentWriteStore(IBehlogEntityFrameworkDbContext db, IBehlogMediator mediator) 
        : base(db, mediator)
    {
    }
}