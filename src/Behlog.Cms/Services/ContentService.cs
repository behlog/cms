namespace Behlog.Cms.Services;


public class ContentService : IContentService
{
    private readonly IBehlogMediator _behlog;
    private readonly IContentReadStore _readStore;
    
    public ContentService(IBehlogMediator behlog, IContentReadStore readStore)
    {
        _behlog = behlog ?? throw new ArgumentNullException(nameof(behlog));
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