using Behlog.Cms.Domain;
using Behlog.Core;

namespace Behlog.Cms.EntityFrameworkCore.Stores;

public class BlockWriteStore : BehlogWriteStore<Block, Guid>, 
    IBlockWriteStore
{
    
    public BlockWriteStore(IBehlogEntityFrameworkDbContext db, IBehlogMediator mediator) 
        : base(db, mediator)
    {
    }
}