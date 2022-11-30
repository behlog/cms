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
            LangId = contentType.LangId,
            Title = contentType.Title,
            Slug = contentType.Slug,
            Status = contentType.Status,
            Description = contentType.Description!,
            CreatedDate = contentType.CreatedDate,
            LastUpdated = contentType.LastUpdated,
            SystemName = contentType.SystemName,
            LastStatusChangedOn = contentType.LastStatusChangedOn,
            LangCode = contentType.Language?.Code,
            LangName = contentType.Language?.Name,
            LangTitle = contentType.Language?.Title
        };
    }
    
}