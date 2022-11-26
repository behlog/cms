using Behlog.Cms.Domain;
using Behlog.Core;

namespace Behlog.Cms.EntityFrameworkCore.Stores;

public class CommentWriteStore : BehlogWriteStore<Comment, Guid>, 
    ICommentWriteStore
{

    public CommentWriteStore(IBehlogEntityFrameworkDbContext db, IBehlogMediator mediator)
        : base(db, mediator)
    {
    }
    
}