using Behlog.Cms.Domain;

namespace Behlog.Cms.EntityFrameworkCore.Stores;

public class BlockReadStore : BehlogReadStore<Block, Guid>, IBlockReadStore
{
    
    public BlockReadStore(IBehlogEntityFrameworkDbContext db) : base(db)
    {
    }
}