using Behlog.Core;
using Behlog.Cms.Contracts;
using Behlog.Cms.Domain;
using Idyfa.Core.Extensions;

namespace Behlog.Cms.Services;


public class ContentService : IContentService
{
    private readonly IBehlogMediator _mediator;
    private readonly IContentReadStore _readStore;
    
    public ContentService(IBehlogMediator mediator, IContentReadStore readStore)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        _readStore = readStore ?? throw new ArgumentNullException(nameof(readStore));
    }
    
    /// <inheritdoc /> 
    public async Task<bool> SlugExistInWebsiteAsync(Guid websiteId, Guid contentId, string slug)
    {
        if (websiteId == default)
            throw new BehlogInvalidEntityIdException(nameof(Website));

        if (slug.IsNullOrEmpty())
            throw new ArgumentNullException(nameof(slug));

        return await _readStore.ExistBySlugAsync(websiteId, slug).ConfigureAwait(false);
    }
}