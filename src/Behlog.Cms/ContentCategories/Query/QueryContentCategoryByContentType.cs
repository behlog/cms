using Behlog.Cms.Models;
using Behlog.Core;

namespace Behlog.Cms.Query;


public class QueryContentCategoryByContentType : IBehlogQuery<IReadOnlyCollection<ContentCategoryResult>>
{

    public QueryContentCategoryByContentType(
        Guid? contentTypeId, Guid? langId)
    {
        ContentTypeId = contentTypeId;
        LangId = langId;
    }
    
    public Guid? ContentTypeId { get; }

    public Guid? LangId { get; }
}