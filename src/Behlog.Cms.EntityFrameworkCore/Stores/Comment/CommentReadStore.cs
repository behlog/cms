using Behlog.Cms.Domain;

namespace Behlog.Cms.EntityFrameworkCore.Stores;

public class CommentReadStore : BehlogReadStore<Comment, Guid>, ICommentReadStore
{
    
    public CommentReadStore(IBehlogEntityFrameworkDbContext db) : base(db)
    {
        
    }
}