using Behlog.Core;
using Behlog.Cms.Models;

namespace Behlog.Cms.Query;


public class QueryContentTypesByLangId : IBehlogQuery<ContentTypeListResult>
{
    public QueryContentTypesByLangId(Guid langId)
    {
        LangId = langId;
    }
    
    public Guid LangId { get; }
}


public class QueryContentTypesByLangCode : IBehlogQuery<ContentTypeListResult>
{

    public QueryContentTypesByLangCode(string langCode)
    {
        LangCode = langCode;
    }
    
    public string LangCode { get; }
}