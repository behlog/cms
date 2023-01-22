namespace Behlog.Cms.Models;

public static class ContentCategoryMappers
{

    public static ContentCategoryResult ToResult(this ContentCategory category)
    {
        category.ThrowExceptionIfArgumentIsNull(nameof(category));

        return new ContentCategoryResult
        {
            Id = category.Id,
            Description = category.Description,
            Slug = category.Slug,
            Status = category.Status,
            Title = category.Title,
            AltTitle = category.AltTitle,
            LangCode = category.Language?.Code,
            LangId = category.LangId,
            LangTitle = category.Language?.Title,
            ParentId = category.ParentId,
            WebsiteId = category.WebsiteId,
            WebsiteName = category.Website?.Name,
            WebsiteTitle = category.Website?.Title,
            ContentTypeId = category.ContentTypeId,
            ContentTypeSlug = category.ContentType?.Slug,
            ContentTypeTitle = category.ContentType?.Title,
            ContentTypeSystemName = category.ContentType?.SystemName
        };
    }
}