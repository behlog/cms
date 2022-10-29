using Behlog.Cms.Domain;
using Behlog.Cms.Repository;

namespace Behlog.Cms.EntityFrameworkCore;

public class ContentRepository : BehlogRepository<Content, Guid>, IContentRepository
{
    
    public ContentRepository(IBehlogEntityFrameworkDbContext db) : base(db)
    {
    }
    
    
}