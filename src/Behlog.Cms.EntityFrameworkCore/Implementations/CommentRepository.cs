using Behlog.Cms.Domain;

namespace Behlog.Cms.EntityFrameworkCore;

public class CommentRepository : BehlogRepository<Comment, Guid>, ICommentRepository
{
    
    public CommentRepository(IBehlogEntityFrameworkDbContext db) : base(db)
    {
    }
}