using Behlog.Cms.Domain;
using Behlog.Cms.Store;
using Behlog.Core;
using Microsoft.EntityFrameworkCore;

namespace Behlog.Cms.EntityFrameworkCore.Stores;

public class ContentCategoryReadStore : BehlogReadStore<ContentCategory, Guid>, IContentCategoryReadStore
{
    public ContentCategoryReadStore(IBehlogEntityFrameworkDbContext db) 
        : base(db)
    {
    }

    public async Task<IReadOnlyCollection<ContentCategory>> FindByContentTypeAsync(
        Guid contentTypeId, CancellationToken cancellationToken = default)
    {
        return await _set.Where(_ => _.ContentTypeId == contentTypeId)
            .ToListAsync(cancellationToken).ConfigureAwait(false);
    }
    
    
}