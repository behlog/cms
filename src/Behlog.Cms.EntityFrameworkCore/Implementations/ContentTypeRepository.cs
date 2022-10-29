using Behlog.Cms.Repository;
using Behlog.Core;

namespace Behlog.Cms.EntityFrameworkCore;

public class ContentTypeRepository : BehlogRepository<ContentType, Guid>, IContentTypeRepository
{
    
    public ContentTypeRepository(IBehlogEntityFrameworkDbContext db) : base(db)
    {
    }
}