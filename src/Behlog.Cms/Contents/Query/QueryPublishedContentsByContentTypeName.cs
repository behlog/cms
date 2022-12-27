namespace Behlog.Cms.Query;


/// <summary>
/// Query <see cref="ContentStatusEnum.Published"/> <see cref="Content"/> list with 
/// pagination and sorting based on <see cref="Website"/> Id and <see cref="ContentType"/> Name.
/// Use it to display a list of Published Contents on the Website with pagination.
/// Includes (Tags and Categories)
/// </summary>
public class QueryPublishedContentsByContentTypeName : IBehlogQuery<QueryResult<ContentResult>>
{
    public QueryPublishedContentsByContentTypeName(
        Guid websiteId, string contentTypeName, QueryOptions options) {

        websiteId.ThrowIfGuidIsEmpty(new BehlogInvalidEntityIdException(nameof(Website)));

        if (contentTypeName.IsNullOrEmptySpace())
            throw new ArgumentNullException(nameof(contentTypeName));

        if (options is null) {
            options = new QueryOptions
            {
                OrderBy = "id",
                OrderDesc = false,
                PageNumber = 1,
                PageSize = 10
            };
        }
    }

    public Guid WebsiteId { get; }
    public QueryOptions Options { get; }
    public string ContentTypeName { get; }
}
