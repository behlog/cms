using Behlog.Core;
using Behlog.Cms.Models;

namespace Behlog.Cms.Query;


public class QueryContentBySlug : IBehlogQuery<ContentResult>
{
    public QueryContentBySlug(Guid websiteId, string slug)
    {
        WebsiteId = websiteId;
        Slug = slug;
    }
    
    public Guid WebsiteId { get; }
    public string Slug { get; }
}