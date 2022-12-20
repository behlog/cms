using Behlog.Core;
using Behlog.Cms.Models;

namespace Behlog.Cms.Query;


public class QueryComponentById : IBehlogQuery<ComponentResult>
{

    public QueryComponentById(Guid componentId)
    {
        Id = componentId;
    }
    
    public Guid Id { get; }
}