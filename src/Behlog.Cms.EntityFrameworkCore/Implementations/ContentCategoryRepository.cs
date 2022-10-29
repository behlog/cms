using Behlog.Cms.Domain;
using Behlog.Cms.Repository;

namespace Behlog.Cms.EntityFrameworkCore;

public class ContentCategoryRepository : BehlogRepository<ContentCategory, Guid>, IContentCategoryRepository
{
    
    public ContentCategoryRepository(IBehlogEntityFrameworkDbContext db) : base(db)
    {
    }
    
}