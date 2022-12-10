using Behlog.Core;
using Behlog.Cms.Models;

namespace Behlog.Cms.Query;


public class QueryContentById : IBehlogQuery<ContentResult>
{
    public QueryContentById(Guid contentId)
    {
        Id = contentId;
    }
    
    public Guid Id { get; }
}