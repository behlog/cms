namespace Behlog.Cms.Query;


public class QueryContentCategoryByContentType : IBehlogQuery<IReadOnlyCollection<ContentCategoryResult>>
{

    public QueryContentCategoryByContentType(
        Guid? contentTypeId, string? contentTypeName, Guid? langId)
    {
        ContentTypeId = contentTypeId;
        ContentTypeName = contentTypeName?.CorrectYeKe();
        LangId = langId;
    }
    
    public Guid? ContentTypeId { get; }
    
    public string? ContentTypeName { get; }

    public Guid? LangId { get; }
}