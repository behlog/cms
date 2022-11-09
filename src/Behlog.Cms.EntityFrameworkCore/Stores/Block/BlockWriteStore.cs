using Behlog.Cms.Domain;

namespace Behlog.Cms.EntityFrameworkCore.Stores;

public class BlockWriteStore : BehlogWriteStore<Block, Guid>, IBlockWriteStore
{
    
    public BlockWriteStore(IBehlogEntityFrameworkDbContext db) : base(db)
    {
    }
}