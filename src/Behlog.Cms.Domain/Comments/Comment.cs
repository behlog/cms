using System;
using Behlog.Cms.Commands;
using Behlog.Core;
using Behlog.Core.Domain;
using Behlog.Extensions;

namespace Behlog.Cms.Domain;

public class Comment : BehlogEntity<Guid>, IHasMetadata
{
    private Comment()
    {
    }

    #region Props

    public string Title { get; protected set; }
    public string Body { get; protected set; }
    public ContentBodyType BodyType { get; protected set; }
    public string Email { get; protected set; }
    public string WebUrl { get; protected set; }
    public string AuthorUserId { get; protected set; }
    public string AuthorName { get; protected set; }
    public Guid ContentId { get; protected set; }
    public DateTime CreatedDate { get; protected set; }
    public DateTime? LastUpdated { get; protected set; }
    public string CreatedByUserId { get; protected set; }
    public string LastUpdatedByUserId { get; protected set; }
    public string CreatedByIp { get; protected set; }
    public string LastUpdatedByIp { get; protected set; }
    #endregion

    #region Navigations

    public Content Content { get; protected set; }

    #endregion

    #region Builders

    public static async Task<Comment> CreateAsync(
        CreateCommentCommand command, IBehlogManager manager)
    {
        command.ThrowExceptionIfArgumentIsNull(nameof(command));
        manager.ThrowExceptionIfArgumentIsNull(nameof(manager));

        var comment = new Comment
        {
            Id = Guid.NewGuid(),
            Title = command.Title?.Trim().CorrectYeKe()!,
            Body = command.Body?.CorrectYeKe().Trim()!,
            Email = command.Email?.Trim()!,
            AuthorName = command.AuthorName?.Trim().CorrectYeKe()!,
            BodyType = command.BodyType,
            ContentId = command.ContentId,
            CreatedDate = DateTime.UtcNow,
            WebUrl = command.WebUrl?.Trim().CorrectYeKe()!,
            AuthorUserId = "", //TODO : read from UserContext
            CreatedByIp = "", //TODO : read from HttpContext
            CreatedByUserId = ""
        };
        
        //TODO : Publish CreatedEvent
        return await Task.FromResult(comment);
    }

    #endregion

}