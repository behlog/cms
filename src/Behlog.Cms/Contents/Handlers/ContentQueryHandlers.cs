using Behlog.Cms.Domain;
using Behlog.Cms.Models;
using Behlog.Cms.Query;
using Behlog.Core;
using Behlog.Extensions;

namespace Behlog.Cms.Handlers;

public class ContentQueryHandlers :
    IBehlogQueryHandler<QueryContentById, ContentResult>,
    IBehlogQueryHandler<QueryContentBySlug, ContentResult>
{
    private readonly IContentReadStore _readStore;
    
    public ContentQueryHandlers(IContentReadStore readStore)
    {
        _readStore = readStore ?? throw new ArgumentNullException(nameof(readStore));
    }
    
    public async Task<ContentResult> HandleAsync(
        QueryContentById query, CancellationToken cancellationToken = default)
    {
        query.ThrowExceptionIfArgumentIsNull(nameof(query));

        throw new NotImplementedException();
    }

    public async Task<ContentResult> HandleAsync(
        QueryContentBySlug query, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}