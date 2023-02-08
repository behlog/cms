namespace Behlog.Cms.ContentCategories;

public interface IContentCategoryService
{

    Task<bool> TitleExistAsync(
        Guid websiteId, Guid contentTypeId, Guid contentCategoryId, string title);


    Task<bool> SlugExistAsync(
        Guid websiteId, Guid contentTypeId, Guid contentCategoryId, string slug);
}