using Behlog.Cms.Domain;
using Behlog.Extensions;

namespace Behlog.Cms.Models;


public class ContentCategoryItemResult
{

    public ContentCategoryItemResult(
        Guid contentId, Guid categoryId)
    {
        ContentId = contentId;
        CategoryId = categoryId;
    }


    public static ContentCategoryItemResult From(ContentCategoryItem item)
    {
        item.ThrowExceptionIfArgumentIsNull(nameof(item));

        var result = new ContentCategoryItemResult(item.ContentId, item.CategoryId);
        return result.WithCategory(item.Category);
    }
    
    public ContentCategoryItemResult WithCategory(ContentCategory category)
    {
        if (category is null) return this;

        Title = category.Title;
        ParentId = category.ParentId;
        Slug = category.Slug;
        LangId = category.LangId;
        return this;
    }
    
    public Guid ContentId { get; }
    public Guid CategoryId { get; }
    
    public string? Title { get; private set; }
    public Guid? ParentId { get; private set; }
    public string? Slug { get; private set; }
    public Guid? LangId { get; private set; }
}