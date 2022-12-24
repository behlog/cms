using Behlog.Core;
using Behlog.Extensions;
using Behlog.Cms.Models;
using Behlog.Cms.Domain;

namespace Behlog.Cms.Query;


public class QueryComponentByName : IBehlogQuery<ComponentResult>
{

    public QueryComponentByName(Guid websiteId, Guid langId, string name)
    {
        websiteId.ThrowIfGuidIsEmpty(new BehlogInvalidEntityIdException(nameof(Component)));
        langId.ThrowIfGuidIsEmpty(new BehlogInvalidEntityIdException(nameof(Language)));
        
        WebsiteId = websiteId;
        LangId = langId;
        Name = name;
    }
    
    public Guid WebsiteId { get; }
    public Guid LangId { get; }
    public string Name { get; }
}