using Behlog.Cms.Domain;
using Behlog.Core;

namespace Behlog.Cms.EntityFrameworkCore.Stores;

public class CommentWriteStore : BehlogEntityFrameworkCoreWriteStore<Comment, Guid>, 
    ICommentWriteStore
{

    public CommentWriteStore(IBehlogEntityFrameworkDbContext db, IBehlogMediator mediator)
        : base(db, mediator)
    {
    }
    
}