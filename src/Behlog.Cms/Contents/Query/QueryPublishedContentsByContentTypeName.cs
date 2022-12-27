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
    public string langCode { get; }
    public QueryOptions Options { get; }
    public string ContentTypeName { get; }
}



public class QueryPublishedContentsByContentTypeNameHandler
    : IBehlogQueryHandler<QueryPublishedContentsByContentTypeName, QueryResult<ContentResult>>
{
    private readonly IContentReadStore _readStore;

    public QueryPublishedContentsByContentTypeNameHandler(IContentReadStore readStore) {
        _readStore = readStore ?? throw new ArgumentNullException(nameof(readStore));
    }


    public async Task<QueryResult<ContentResult>> HandleAsync(
        QueryPublishedContentsByContentTypeName query, CancellationToken cancellationToken = default) {
        query.ThrowExceptionIfArgumentIsNull(nameof(query));

        //TODO: read LangId from Database.
        var langId = BehlogSupportedLanguages.GetIdByCode(query.langCode);

        var result = await _readStore.QueryAsync(
            query.WebsiteId, langId, query.ContentTypeName, 
            ContentStatusEnum.Published,query.Options, cancellationToken).ConfigureAwait(false);

        return QueryResult<ContentResult>
            .Create(result.Results.Select(_ => _.ToResult()))
            .WithPageNumber(result.PageNumber)
            .WithPageSize(result.PageSize)
            .WithTotalCount(result.TotalCount);
    }
}