using Behlog.Cms.Domain;
using Behlog.Cms.Models;
using Behlog.Core;
using Behlog.Core.Models;

namespace Behlog.Cms.Query;


public class QueryContentsFiltered : IBehlogQuery<QueryResult<ContentResult>>
{
    public QueryContentsFiltered(Guid websiteId)
    {
        WebsiteId = websiteId;
        Filter = new QueryFilter();
    }
    
    public Guid WebsiteId { get; }
    
    public QueryFilter Filter { get; set; }
    
    public Guid? LangId { get; set; }
    
    public string? AuthorUserId { get; set; }
    
    public ContentStatus? Status { get; set; }
    
    public string? Search { get; set; }
    
    public string? Title { get; set; }
}