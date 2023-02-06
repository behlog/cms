namespace Behlog.Cms.Query;


public class QueryContentByWebsiteAndContentType : IBehlogQuery<QueryResult<ContentResult>>
{
    public QueryContentByWebsiteAndContentType(
        Guid websiteId, Guid langId, Guid contentTypeId, ContentStatusEnum? status = null, QueryOptions options = null)
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
            Options = QueryOptions.New()
                .WillOrderBy("id")
                .WithPageSize(1)
                .WithPageSize(10)
                .WillOrderDesc();
        }

        Status = status;
    }
    
    public Guid WebsiteId { get; }
    public Guid LangId { get; }
    public Guid ContentTypeId { get; }
    public ContentStatusEnum? Status { get; }
    public QueryOptions Options { get; }
}