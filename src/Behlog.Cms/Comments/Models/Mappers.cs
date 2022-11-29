using Behlog.Cms.Domain;
using Behlog.Cms.Domain.Models;
using Behlog.Extensions;

namespace Behlog.Cms.Models;

public static class CommentMappers
{

    public static CommentResult ToResult(this Comment comment)
    {
        comment.ThrowExceptionIfArgumentIsNull(nameof(comment));

        return new CommentResult
        {
            Id = comment.Id,
            Body = comment.Body,
            Email = comment.Email,
            Title = comment.Title,
            AuthorName = comment.AuthorName,
            BodyType = comment.BodyType,
            CreatedDate = comment.CreatedDate,
            LastUpdated = comment.LastUpdated,
            WebUrl = comment.WebUrl,
            AuthorUserId = comment.AuthorUserId,
            CreatedByIp = comment.CreatedByIp,
            CreatedByUserId = comment.CreatedByUserId,
            LastUpdatedByIp = comment.LastUpdatedByIp,
            LastUpdatedByUserId = comment.LastUpdatedByUserId
        };
    }
}