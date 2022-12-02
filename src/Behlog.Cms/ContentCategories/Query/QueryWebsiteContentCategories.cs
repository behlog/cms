using Behlog.Core;
using Behlog.Extensions;
using Behlog.Cms.Domain;
using Behlog.Cms.Models;

namespace Behlog.Cms.Query;


public class QueryWebsiteContentCategories : IBehlogQuery<ContentCategoryListResult>
{
    public QueryWebsiteContentCategories(Guid websiteId, Guid? contentTypeId = null)
    {
        websiteId.ThrowIfGuidIsEmpty(
            new BehlogInvalidEntityIdException(nameof(Website))
            );
        WebsiteId = websiteId;
        ContentTypeId = contentTypeId;
    }
    
    public Guid WebsiteId { get; }
    
    public Guid? ContentTypeId { get; }
}