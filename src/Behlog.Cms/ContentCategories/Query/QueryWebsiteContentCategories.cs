using Behlog.Core;
using Behlog.Extensions;
using Behlog.Cms.Domain;
using Behlog.Cms.Models;

namespace Behlog.Cms.Query;


public class QueryWebsiteContentCategories : IBehlogQuery<ContentCategoryListResult>
{
    public QueryWebsiteContentCategories(Guid websiteId)
    {
        websiteId.ThrowIfGuidIsEmpty(
            new BehlogInvalidEntityIdException(nameof(Website))
            );
        WebsiteId = websiteId;
    }
    
    public Guid WebsiteId { get; }
}