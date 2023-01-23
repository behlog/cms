using Behlog.Core;
using Behlog.Cms.Models;
using Behlog.Core.Models;

namespace Behlog.Cms.Query;


public class QueryFileUploadsByWebsiteId : IBehlogQuery<QueryResult<FileUploadResult>>
{
    public QueryFileUploadsByWebsiteId(Guid websiteId)
    {
        WebsiteId = websiteId;
        Filter = QueryOptions.New()
            .WillOrderBy("id")
            .WithPageSize(1)
            .WithPageSize(10)
            .WillOrderDesc();
    }
    
    public Guid WebsiteId { get; }
    
    public QueryOptions Filter { get; set; }
}