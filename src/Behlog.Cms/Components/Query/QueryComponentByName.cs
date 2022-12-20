using Behlog.Core;
using Behlog.Extensions;
using Behlog.Cms.Models;
using Behlog.Cms.Domain;

namespace Behlog.Cms.Query;


public class QueryComponentByName : IBehlogQuery<ComponentResult>
{

    public QueryComponentByName(Guid websiteId, string name)
    {
        WebsiteId.ThrowIfGuidIsEmpty(new BehlogInvalidEntityIdException(nameof(Component)));
        
        WebsiteId = websiteId;
        Name = name;
    }
    
    public Guid WebsiteId { get; }
    public string Name { get; }
}