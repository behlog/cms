namespace Behlog.Cms.Handlers;


public class ContentTypeQueryHandlers :
    IBehlogQueryHandler<QueryContentTypeById, ContentTypeResult>,
    IBehlogQueryHandler<QueryContentTypesByLangId, ContentTypeListResult>,
    IBehlogQueryHandler<QueryContentTypesByLangCode, ContentTypeListResult>,
    IBehlogQueryHandler<QueryContentTypeBySystemName, ContentTypeResult>,
    IBehlogQueryHandler<QueryActiveContentType, ContentTypeResult?>,
    IBehlogQueryHandler<QueryAdminContentType, QueryResult<ContentTypeResult>>
{
    private readonly IContentTypeReadStore _readStore;
    private readonly ILanguageReadStore _langReadStore;

    public ContentTypeQueryHandlers(
        IContentTypeReadStore readStore, ILanguageReadStore langReadStore)
    {
        _readStore = readStore ?? throw new ArgumentNullException(nameof(readStore));
        _langReadStore = langReadStore ?? throw new ArgumentNullException(nameof(langReadStore));
    }
    
    
    public async Task<ContentTypeResult> HandleAsync(
        QueryContentTypeById query, CancellationToken cancellationToken = default)
    {
        query.ThrowExceptionIfArgumentIsNull(nameof(query));

        var contentType = await _readStore.GetByIdAsync(query.Id, cancellationToken).ConfigureAwait(false);
        contentType.ThrowExceptionIfReferenceIsNull(nameof(contentType));

        return await Task.FromResult(contentType.ToResult());
    }
    
    
    public async Task<ContentTypeListResult> HandleAsync(
        QueryContentTypesByLangId query, CancellationToken cancellationToken = default)
    {
        query.ThrowExceptionIfArgumentIsNull(nameof(query));

        var lang = await _langReadStore.FindAsync(query.LangId, cancellationToken).ConfigureAwait(false);
        lang.ThrowExceptionIfReferenceIsNull(nameof(lang));
        
        var source = await _readStore.GetByLangIdAsync(lang.Id, cancellationToken).ConfigureAwait(false);
        if (source is null || !source.Any())
            return new ContentTypeListResult(lang.Id,lang.Code, lang.Title);
        
        return new ContentTypeListResult(
            lang.Id, lang.Code, lang.Title,
            source.Select(_ => _.ToResult()).ToList()
            );
    }

    public async Task<ContentTypeListResult> HandleAsync(
        QueryContentTypesByLangCode query, CancellationToken cancellationToken = default)
    {
        query.ThrowExceptionIfArgumentIsNull(nameof(query));

        var lang = await _langReadStore.GetByCodeAsync(query.LangCode, cancellationToken).ConfigureAwait(false);
        lang.ThrowExceptionIfReferenceIsNull(nameof(lang));

        var source = await _readStore.GetByLangIdAsync(lang.Id, cancellationToken).ConfigureAwait(false);
        if (source is null || !source.Any())
            return new ContentTypeListResult(lang.Id, lang.Code, lang.Title);

        return new ContentTypeListResult(
            lang.Id, lang.Code, lang.Title,
            source.Select(_ => _.ToResult()).ToList()
            );
    }


    public async Task<ContentTypeResult> HandleAsync(
        QueryContentTypeBySystemName query, CancellationToken cancellationToken = default)
    {
        query.ThrowExceptionIfArgumentIsNull(nameof(query));

        var contentType = await _readStore.GetBySystemNameAsync(
            query.LangId, query.SystemName, cancellationToken).ConfigureAwait(false);
        contentType.ThrowExceptionIfReferenceIsNull(nameof(contentType));

        return contentType!.ToResult();
    }

    public async Task<QueryResult<ContentTypeResult>> HandleAsync(
        QueryAdminContentType query, CancellationToken cancellationToken = default) {
        query.ThrowExceptionIfArgumentIsNull(nameof(query));

        var result = await _readStore.QueryAsync(query, cancellationToken).ConfigureAwait(false);

        return QueryResult<ContentTypeResult>
            .Create(result.Results.Select(_ => _.ToResult()))
            .WithPageSize(result.PageSize)
            .WithPageNumber(result.PageNumber)
            .WithTotalCount(result.TotalCount);
    }

    public async Task<ContentTypeResult?> HandleAsync(
        QueryActiveContentType query, CancellationToken cancellationToken = default) {
        query.ThrowExceptionIfArgumentIsNull(nameof(query));

        var result = await _readStore.QueryAsync(query, cancellationToken).ConfigureAwait(false);

        return await Task.FromResult(result?.ToResult());
    }
}