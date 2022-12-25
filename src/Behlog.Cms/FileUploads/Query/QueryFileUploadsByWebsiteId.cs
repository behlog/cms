using Behlog.Core;
using Behlog.Cms.Models;
using Behlog.Core.Models;

namespace Behlog.Cms.Query;


public class QueryFileUploadsByWebsiteId : IBehlogQuery<QueryResult<FileUploadResult>>
{
    public QueryFileUploadsByWebsiteId(Guid websiteId)
    {
        WebsiteId = websiteId;
        Filter = new QueryOptions();
    }
    
    public Guid WebsiteId { get; }
    
    public QueryOptions Filter { get; set; }
}