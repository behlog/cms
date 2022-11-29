using Behlog.Cms.Models;
using Behlog.Cms.Query;
using Behlog.Cms.Store;
using Behlog.Core;
using Behlog.Extensions;

namespace Behlog.Cms.Handlers;

public class WebsiteQueryHandlers :
    IBehlogQueryHandler<QueryWebsiteById, WebsiteResult>,
    IBehlogQueryHandler<QueryDefaultWebsite, WebsiteResult>
{
    private readonly IWebsiteReadStore _readStore;
    
    public WebsiteQueryHandlers(
        IWebsiteReadStore readStore)
    {
        _readStore = readStore ?? throw new ArgumentNullException(nameof(readStore));
    }

    public async Task<WebsiteResult> HandleAsync(
        QueryWebsiteById query, CancellationToken cancellationToken = new CancellationToken())
    {
        query.ThrowExceptionIfArgumentIsNull(nameof(query));

        var website = await _readStore.GetByIdAsync(
            query.Id, 
            cancellationToken).ConfigureAwait(false);
        
        website.ThrowExceptionIfReferenceIsNull(nameof(website));

        return await Task.FromResult(website.ToResult());
    }


    public async Task<WebsiteResult> HandleAsync(
        QueryDefaultWebsite query, CancellationToken cancellationToken = default)
    {
        query.ThrowExceptionIfArgumentIsNull(nameof(query));

        var website = await _readStore.GetDefaultAsync(cancellationToken).ConfigureAwait(false);
        website.ThrowExceptionIfReferenceIsNull(nameof(website));

        return await Task.FromResult(website.ToResult());
    }
}