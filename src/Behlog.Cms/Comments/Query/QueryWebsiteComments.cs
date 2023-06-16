namespace Behlog.Cms.Query;


public class QueryWebsiteComments : IBehlogQuery<QueryResult<CommentResult>>
{
    public QueryWebsiteComments(
        Guid websiteId, CommentStatusEnum? status, QueryOptions? options = null)
    {
        WebsiteId = websiteId;
        Status = status;
        Options = options ?? QueryOptions.New()
            .WillOrderBy("id")
            .WithPageSize(1)
            .WithPageSize(10)
            .WillOrderDesc();
    }
    
    public Guid WebsiteId { get; }
    public CommentStatusEnum? Status { get; }
    public QueryOptions Options { get; }
}