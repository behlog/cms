namespace Behlog.Cms.Query;

public class QueryCommentById : IBehlogQuery<CommentResult>
{
    public QueryCommentById(Guid id)
    {
        Id = id;
    }
    
    public Guid Id { get; }
}