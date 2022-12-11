using Behlog.Core;
using Behlog.Cms.Models;

namespace Behlog.Cms.Query;


public class QueryContentCategoryByParentId : IBehlogQuery<IReadOnlyCollection<ContentCategoryResult>>
{
    public QueryContentCategoryByParentId(Guid? parentId = null)
    {
        ParentId = parentId;
    }
    
    public Guid LangId { get; }

    public Guid? ParentId { get; }
}