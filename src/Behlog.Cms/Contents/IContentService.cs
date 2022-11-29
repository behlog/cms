namespace Behlog.Cms.Contracts;

public interface IContentService
{

    Task<bool> SlugExistedInWebsiteAsync(
        Guid websiteId, Guid? contentId, string slug);
    
    
}