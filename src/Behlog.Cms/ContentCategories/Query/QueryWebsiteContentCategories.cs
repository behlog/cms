namespace Behlog.Cms.Query;


public class QueryWebsiteContentCategories : IBehlogQuery<ContentCategoryListResult>
{
    public QueryWebsiteContentCategories(
        Guid websiteId, Guid langId, Guid? contentTypeId = null)
    {
        websiteId.ThrowIfGuidIsEmpty(
            new BehlogInvalidEntityIdException(nameof(Website))
            );
        WebsiteId = websiteId;
        LangId = langId;
        ContentTypeId = contentTypeId;
    }
    
    public Guid WebsiteId { get; }
    
    public Guid LangId { get; }

    public Guid? ContentTypeId { get; }
}