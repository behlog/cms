namespace Behlog.Cms.Query;


public class QueryFileUploadsByWebsiteId : IBehlogQuery<QueryResult<FileUploadResult>>
{
    public QueryFileUploadsByWebsiteId(Guid websiteId)
    {
        websiteId.ThrowIfGuidIsEmpty(new BehlogInvalidEntityIdException(nameof(Website)));
        WebsiteId = websiteId;
        Options = QueryOptions.New()
            .WillOrderBy("id").WithPageSize(1)
            .WithPageSize(10).WillOrderDesc();
    }

    public QueryFileUploadsByWebsiteId(Guid websiteId, QueryOptions options)
    {
        websiteId.ThrowIfGuidIsEmpty(new BehlogInvalidEntityIdException(nameof(Website)));
        WebsiteId = websiteId;

        Options = options;
        if (Options is null)
        {
            Options = QueryOptions.New()
                .WillOrderBy("id").WithPageSize(1)
                .WithPageSize(10).WillOrderDesc();
        }
    }
    
    public Guid WebsiteId { get; }
    
    public QueryOptions Options { get; set; }
}