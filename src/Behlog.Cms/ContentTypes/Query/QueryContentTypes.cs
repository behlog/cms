using Behlog.Core;
using Behlog.Cms.Models;

namespace Behlog.Cms.Query;


public class QueryContentTypes : IBehlogQuery<ContentTypeListResult>
{
    public QueryContentTypes(Guid langId)
    {
        LangId = langId;
    }
    
    public Guid LangId { get; }
}