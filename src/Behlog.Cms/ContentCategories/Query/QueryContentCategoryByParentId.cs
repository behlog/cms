namespace Behlog.Cms.Query;


public class QueryContentCategoryByParentId : IBehlogQuery<IReadOnlyCollection<ContentCategoryResult>>
{
    public QueryContentCategoryByParentId(Guid langId, Guid? parentId = null)
    {
        LangId = langId;
        ParentId = parentId;
    }
    
    public Guid LangId { get; }

    public Guid? ParentId { get; }
}