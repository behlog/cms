using Behlog.Core;
using Behlog.Cms.Query;
using Behlog.Cms.Store;
using Behlog.Extensions;
using Behlog.Cms.Models;

namespace Behlog.Cms.Handlers;


public class ContentTypeQueryHandlers :
    IBehlogQueryHandler<QueryContentTypes, ContentTypeListResult>
{
    private readonly IContentTypeReadStore _readStore;


    public ContentTypeQueryHandlers(IContentTypeReadStore readStore)
    {
        _readStore = readStore ?? throw new ArgumentNullException(nameof(readStore));
    }
    
    
    public async Task<ContentTypeListResult> HandleAsync(
        QueryContentTypes query, CancellationToken cancellationToken = default)
    {
        query.ThrowExceptionIfArgumentIsNull(nameof(query));

        var source = await _readStore.GetByLangIdAsync(query.LangId).ConfigureAwait(false);
        if (source is null || !source.Any())
            return new ContentTypeListResult();
        
        return new ContentTypeListResult(
            source.Select(_ => _.ToResult()).ToList()
            );
    }
}