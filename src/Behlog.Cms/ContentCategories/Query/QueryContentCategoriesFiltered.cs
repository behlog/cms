namespace Behlog.Cms.Query;

public class QueryContentCategoriesFiltered : IBehlogQuery<QueryResult<ContentCategoryResult>>
{

    public QueryContentCategoriesFiltered(
        Guid websiteId, Guid langId, Guid contentTypeId,
        EntityStatusEnum? status = null, QueryOptions? options = null)
    {
        websiteId.ThrowIfGuidIsEmpty(new BehlogInvalidEntityIdException(nameof(Website)));
        WebsiteId = websiteId;

        langId.ThrowIfGuidIsEmpty(new BehlogInvalidEntityIdException(nameof(Language)));
        LangId = langId;

        contentTypeId.ThrowIfGuidIsEmpty(new BehlogInvalidEntityIdException(nameof(contentTypeId)));
        ContentTypeId = contentTypeId;

        Status = status;

        if (options is null)
        {
            options = QueryOptions.New()
                .WithPageNumber(1)
                .WithPageSize(10)
                .WillOrderBy("id")
                .WillOrderDesc();
        }

        Options = options;
    }
    
    public Guid WebsiteId { get; }
    
    public Guid ContentTypeId { get; }
    
    public Guid LangId { get; }
    
    public EntityStatusEnum? Status { get; }
    
    public QueryOptions Options { get; }
}