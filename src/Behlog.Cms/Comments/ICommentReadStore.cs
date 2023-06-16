namespace Behlog.Cms.Domain;

public interface ICommentReadStore : IBehlogReadStore<Comment, Guid>
{
    
    /// <summary>
    /// Query for <see cref="Comment"/> in a specified <see cref="Website"/>
    /// with filter and pagination support.
    /// </summary>
    /// <param name="query"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<QueryResult<Comment>> QueryAsync(
        QueryWebsiteComments model, CancellationToken cancellationToken = default);
}