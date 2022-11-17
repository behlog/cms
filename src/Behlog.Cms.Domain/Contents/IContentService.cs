namespace Behlog.Cms.Domain;

public interface IContentService
{

    Task<bool> SlugExistedInWebsiteAsync(
        Guid websiteId, Guid? contentId, string slug);
    
    
}