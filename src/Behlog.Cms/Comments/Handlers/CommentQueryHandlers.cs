namespace Behlog.Cms.Handlers;

public class CommentQueryHandlers :
    IBehlogQueryHandler<QueryWebsiteComments, QueryResult<CommentResult>>
{
    private readonly ICommentReadStore _readStore;

    public CommentQueryHandlers(ICommentReadStore readStore)
    {
        _readStore = readStore ?? throw new ArgumentNullException(nameof(readStore));
    }
    
    public async Task<QueryResult<CommentResult>> HandleAsync(
        QueryWebsiteComments query, CancellationToken cancellationToken = default)
    {
        query.ThrowExceptionIfArgumentIsNull(nameof(query));

        var result = await _readStore.QueryAsync(query, cancellationToken).ConfigureAwait(false);
        
        return QueryResult<CommentResult>
            .Create(result.Results.Select(_=> _.ToResult()))
            .WithPageNumber(result.PageNumber)
            .WithPageSize(result.PageSize)
            .WithTotalCount(result.TotalCount);
    }
}