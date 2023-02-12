using Behlog.Extensions;
using Behlog.Cms.Domain;

namespace Behlog.Cms.Models;


public static class ContentMappers
{

    public static ContentResult ToResult(this Content content)
    {
        content.ThrowExceptionIfArgumentIsNull(nameof(content));

        var result = new ContentResult(
            content.Id, content.Title, content.Slug, content.ContentTypeId, content.LangId,
            content.Body!, content.BodyType, content.AuthorUserId, content.Summary!, content.Status,
            content.AltTitle!, content.OrderNum, content.LastStatusChangedDate, content.PublishDate,
            content.CoverPhoto, content.IconName, content.ViewPath, content.CreatedDate, content.LastUpdated, 
            content.CreatedByUserId, content.LastUpdatedByUserId, content.CreatedByIp, content.LastUpdatedByIp);

        return result
            .WithCategories(content.Categories?.ToList()!)
            .WithContentType(content.ContentType)
            .WithFiles(content.Files?.ToList()!)
            .WithLanguage(content.Language)
            .WithMeta(content.Meta?.ToList()!)
            .WithTags(content.Tags?.ToList()!);
    }


    public static MetaResult ToResult(this ContentMeta meta)
    {
        meta.ThrowExceptionIfArgumentIsNull(nameof(meta));

        return new MetaResult(
            meta.OwnerId, meta.Title!, meta.MetaKey, meta.MetaValue!, meta.MetaType!,
            meta.Status, meta.LangId, meta.Description!, meta.Category!, meta.OrderNum);
    }

    public static ICollection<ContentTag>? GetContentTags(this IReadOnlyCollection<Guid> tags, Guid contentId)
    {
        return tags?.ToList()
            .Select(tagId => new ContentTag(contentId, tagId)).ToList()!;
    }
}