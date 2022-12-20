using Behlog.Cms.Models;
using Behlog.Core;

namespace Behlog.Cms.Components.Query;


public class QueryComponentById : IBehlogQuery<ComponentResult>
{

    public QueryComponentById(Guid componentId)
    {
        Id = componentId;
    }
    
    public Guid Id { get; }
}