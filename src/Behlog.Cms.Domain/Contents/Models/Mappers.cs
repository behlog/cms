using Behlog.Cms.Domain;
using Behlog.Extensions;

namespace Behlog.Cms.Models;

public static class ContentMappers
{

    public static ContentResult ToResult(this Content content)
    {
        content.ThrowExceptionIfArgumentIsNull(nameof(content));
        
        return new ContentResult(
            content.Id, content.Title, content.Slug, content.ContentTypeId,
            "", "", content.Body, content.BodyType, 
            content.AuthorUserId, content.Summary, content.Status, content.AltTitle, 
            content.OrderNum, content.Categories);
    }
}