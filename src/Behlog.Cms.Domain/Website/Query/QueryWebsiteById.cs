using Behlog.Core;
using Behlog.Cms.Models;

namespace Behlog.Cms.Query;


public class QueryWebsiteById : IBehlogQuery<WebsiteResult>
{
    public QueryWebsiteById(Guid websiteId)
    {
        if (websiteId == default)
            throw new BehlogInvalidEntityIdException();
        
        Id = websiteId;
    }
    
    public Guid Id { get; }
}