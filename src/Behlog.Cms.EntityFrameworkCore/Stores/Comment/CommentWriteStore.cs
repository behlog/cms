using Behlog.Cms.Domain;

namespace Behlog.Cms.EntityFrameworkCore.Stores;

public class CommentWriteStore : BehlogWriteStore<Comment, Guid>, ICommentWriteStore
{

    public CommentWriteStore(IBehlogEntityFrameworkDbContext db)
        : base(db)
    {
    }
    
}