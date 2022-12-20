using Behlog.Cms.Exceptions;
using Behlog.Cms.Contracts;

namespace Behlog.Cms.Domain;


public partial class Content
{
    
    private static async Task GuardAgainstDuplicateSlug(
        Guid contentId, Guid websiteId, string slug, IContentService service)
    {
        if (await service.SlugExistInWebsiteAsync(websiteId, contentId, slug))
            throw new ContentSlugAlreadyExistedException(websiteId, slug);
    }
}