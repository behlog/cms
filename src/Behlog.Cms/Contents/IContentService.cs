using Behlog.Cms.Domain;

namespace Behlog.Cms.Contracts;

public interface IContentService
{

    /// <summary>
    /// Checks if <see cref="Content"/> with the given Slug already exists in the Website with the given Id or not.
    /// </summary>
    /// <param name="websiteId"></param>
    /// <param name="contentId"></param>
    /// <param name="slug"></param>
    /// <returns></returns>
    Task<bool> SlugExistedInWebsiteAsync(
        Guid websiteId, Guid contentId, string slug);
    
    
}