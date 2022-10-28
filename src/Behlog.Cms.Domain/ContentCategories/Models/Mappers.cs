using Behlog.Cms.Domain;
using Behlog.Extensions;

namespace Behlog.Cms.Models;

public static class ContentCategoryMappers
{

    public static ContentCategoryResult ToResult(this ContentCategory category)
    {
        category.ThrowExceptionIfArgumentIsNull(nameof(category));

        return new ContentCategoryResult(category.Id, category.Title,
            category.AltTitle, category.Slug, category.ParentId,
            category.ContentTypeId, category.Status, category.Description);
    }
}