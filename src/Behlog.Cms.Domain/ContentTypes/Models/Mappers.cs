using Behlog.Core;
using Behlog.Extensions;

namespace Behlog.Cms.Models;

public static class ContentTypeMappers
{

    public static ContentTypeResult ToResult(this ContentType contentType)
    {
        contentType.ThrowExceptionIfArgumentIsNull(nameof(contentType));

        return new ContentTypeResult
        {
            Id = contentType.Id,
            Lang = contentType.Lang,
            Title = contentType.Title,
            Slug = contentType.Slug,
            Status = contentType.Status,
            Description = contentType.Description,
            CreatedDate = contentType.CreatedDate,
            LastUpdated = contentType.LastUpdated,
            SystemName = contentType.SystemName,
            LastStatusChangedOn = contentType.LastStatusChangedOn
        };
    }
    
}