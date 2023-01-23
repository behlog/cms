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
        Guid websiteId, string langCode, string contentTypeName, QueryOptions options) {

        websiteId.ThrowIfGuidIsEmpty(new BehlogInvalidEntityIdException(nameof(Website)));

        if (langCode.IsNullOrEmptySpace())
            throw new ArgumentNullException(nameof(langCode));

        if (contentTypeName.IsNullOrEmptySpace())
            throw new ArgumentNullException(nameof(contentTypeName));

        if (options is null) {
            options = QueryOptions.New()
                .WillOrderBy("id")
                .WithPageSize(1)
                .WithPageSize(10)
                .WillOrderDesc();
        }

        WebsiteId = websiteId;
        LangCode = langCode;
        ContentTypeName = contentTypeName;
        Options = options;
    }

    public Guid WebsiteId { get; }
    public string LangCode { get; }
    public QueryOptions Options { get; }
    public string ContentTypeName { get; }
}
