using Behlog.Extensions;
using Behlog.Cms.Domain;

namespace Behlog.Cms.Models;


public static class ContentMappers
{

    public static ContentResult ToResult(this Content content)
    {
        content.ThrowExceptionIfArgumentIsNull(nameof(content));

        return new ContentResult(
            content.Id, content.Title, content.Slug, content.ContentTypeId,
            "", "", content.Body!, content.BodyType,
            content.AuthorUserId, content.Summary!, content.Status, 
            content.AltTitle!, content.OrderNum, 
            content.Categories?.Select(
                category => category.CategoryId).ToList()!,
            content.Meta?.Select(_=> _.ToResult()).ToList()!
            );
    }


    public static MetaResult ToResult(this ContentMeta meta)
    {
        meta.ThrowExceptionIfArgumentIsNull(nameof(meta));

        return new MetaResult
        {
            OwnerId = meta.OwnerId,
            MetaKey = meta.MetaKey,
            MetaValue = meta.MetaValue,
            Category = meta.Category,
            Description = meta.Description,
            Status = meta.Status,
            Title = meta.Title,
            LangCode = meta.LangCode,
            LangId = meta.LangId,
            MetaType = meta.MetaType,
            OrderNum = meta.OrderNum
        };
    }
}