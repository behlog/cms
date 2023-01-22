namespace Behlog.Cms.Query;


public class QueryContentByWebsiteAndContentType : IBehlogQuery<QueryResult<ContentResult>>
{
    public QueryContentByWebsiteAndContentType(
        Guid websiteId, Guid langId, Guid contentTypeId, ContentStatus? status = null, QueryOptions options = null)
    {
        WebsiteId = websiteId;
        WebsiteId.ThrowIfGuidIsEmpty(new BehlogInvalidEntityIdException(nameof(Website)));
        
        LangId = langId;
        LangId.ThrowIfGuidIsEmpty(new BehlogInvalidEntityIdException(nameof(Language)));
        
        ContentTypeId = contentTypeId;
        ContentTypeId.ThrowIfGuidIsEmpty(new BehlogInvalidEntityIdException(nameof(ContentType)));

        Options = options;
        if (Options is null)
        {
            Options = new QueryOptions
            {
                OrderBy = "id",
                OrderDesc = true,
                PageNumber = 1,
                PageSize = 10
            };
        }
    }
    
    public Guid WebsiteId { get; }
    public Guid LangId { get; }
    public Guid ContentTypeId { get; }
    public ContentStatusEnum? Status { get; }
    public QueryOptions Options { get; }
}