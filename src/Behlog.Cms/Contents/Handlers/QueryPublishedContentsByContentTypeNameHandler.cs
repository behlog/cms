namespace Behlog.Cms.Handlers;

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
        var langId = BehlogSupportedLanguages.GetIdByCode(query.LangCode);

        var result = await _readStore.QueryAsync(
            query.WebsiteId, langId, query.ContentTypeName,
            ContentStatusEnum.Published, query.Options, cancellationToken).ConfigureAwait(false);

        return QueryResult<ContentResult>
            .Create(result.Results.Select(_ => _.ToResult()))
            .WithPageNumber(result.PageNumber)
            .WithPageSize(result.PageSize)
            .WithTotalCount(result.TotalCount);
    }
}