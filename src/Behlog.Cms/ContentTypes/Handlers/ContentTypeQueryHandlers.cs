using Behlog.Core;
using Behlog.Cms.Query;
using Behlog.Cms.Store;
using Behlog.Extensions;
using Behlog.Cms.Models;

namespace Behlog.Cms.Handlers;


public class ContentTypeQueryHandlers :
    IBehlogQueryHandler<QueryContentTypeById, ContentTypeResult>,
    IBehlogQueryHandler<QueryContentTypesByLangId, ContentTypeListResult>,
    IBehlogQueryHandler<QueryContentTypesByLangCode, ContentTypeListResult>
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

    
}