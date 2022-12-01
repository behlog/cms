using Behlog.Cms.Models;
using Behlog.Core;

namespace Behlog.Cms.Query;


public class QueryContentCategoryByContentType : IBehlogQuery<IReadOnlyCollection<ContentCategoryResult>>
{

    public QueryContentCategoryByContentType(Guid? contentTypeId = null)
    {
        ContentTypeId = contentTypeId;
    }
    
    public Guid? ContentTypeId { get; }
}