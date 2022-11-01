using Behlog.Core;
using Behlog.Extensions;
using Behlog.Cms.Events;
using Behlog.Core.Domain;
using Behlog.Cms.Commands;

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
    public CommentStatus Status { get; protected set; }
    public string Email { get; protected set; }
    public string WebUrl { get; protected set; }
    public string AuthorUserId { get; protected set; }
    public string AuthorName { get; protected set; }
    public Guid ContentId { get; protected set; }
    public DateTime CreatedDate { get; protected set; }
    public DateTime? LastUpdated { get; protected set; }
    public DateTime? LastStatusChangedOn { get; protected set; }
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
            Status = CommentStatus.Created,
            ContentId = command.ContentId,
            CreatedDate = DateTime.UtcNow,
            WebUrl = command.WebUrl?.Trim().CorrectYeKe()!,
            AuthorUserId = "", //TODO : read from UserContext
            CreatedByIp = "", //TODO : read from HttpContext
            CreatedByUserId = ""
        };
        
        await comment.PublishCreatedEvent(manager);
        return await Task.FromResult(comment);
    }


    public async Task UpdateAsync(
        UpdateCommentCommand command, IBehlogManager manager)
    {
        command.ThrowExceptionIfArgumentIsNull(nameof(command));
        manager.ThrowExceptionIfArgumentIsNull(nameof(manager));

        Title = command.Title?.Trim().CorrectYeKe()!;
        Body = command.Body?.CorrectYeKe()!;
        BodyType = command.BodyType;
        Email = command.Email?.Trim().CorrectYeKe()!;
        WebUrl = command.WebUrl?.Trim().CorrectYeKe()!;
        LastUpdated = DateTime.UtcNow;
        LastUpdatedByIp = ""; //TODO : read from Context
        LastUpdatedByUserId = ""; //TODO : read from context

        await PublishUpdatedEvent(manager);
    }
    
    public async Task SoftDeleteAsync(IBehlogManager manager)
    {
        //TODO : Check if comment can be soft deleted
        ChangeStatus(CommentStatus.Deleted);

        await PublishSoftDeletedEvent(manager);
    }


    public async Task RemoveAsync(IBehlogManager manager)
    {
        await PublishRemovedEvent(manager);
    }

    /// <summary>
    /// Approve the <see cref="Comment"/>
    /// </summary>
    /// <param name="manager"></param>
    public async Task ApproveAsync(IBehlogManager manager)
    {
        if(Status == CommentStatus.Approved) return;
        
        ChangeStatus(CommentStatus.Approved);
        await PublishApprovedEvent(manager);
    }

    /// <summary>
    /// Reject the <see cref="Comment"/>
    /// </summary>
    /// <param name="manager"></param>
    public async Task RejectAsync(IBehlogManager manager)
    {
        if(Status == CommentStatus.Rejected) return;
        
        ChangeStatus(CommentStatus.Rejected);
        await PublishRejectedEvent(manager);
    }


    public async Task BlockAsync(IBehlogManager manager)
    {
        if(Status == CommentStatus.Blocked) return;
        
        ChangeStatus(CommentStatus.Blocked);
        await PublishBlockedEvent(manager);
    }


    public async Task MarkAsSpamAsync(IBehlogManager manager)
    {
        if(Status == CommentStatus.Spam) return;
        
        ChangeStatus(CommentStatus.Spam);
        await PublishSpammedEvent(manager);
    }

    #endregion

    #region Event Publishers

    private async Task PublishCreatedEvent(IBehlogManager manager)
    {
        var e = new CommentCreatedEvent(
            Id, Title, Body, BodyType, Email, WebUrl, AuthorUserId,
            AuthorName, CreatedByUserId, CreatedByIp, CreatedDate);
        await manager.PublishAsync(e).ConfigureAwait(false);
    }


    private async Task PublishUpdatedEvent(IBehlogManager manager)
    {
        var e = new CommentUpdatedEvent(
            Id, Title, Body, BodyType, Email, WebUrl, AuthorUserId,
            AuthorName, CreatedByUserId, LastUpdatedByUserId,
            CreatedByIp, LastUpdatedByIp, CreatedDate, LastUpdated);
        await manager.PublishAsync(e).ConfigureAwait(false);
    }

    private async Task PublishRemovedEvent(IBehlogManager manager)
    {
        var e = new CommentRemovedEvent(Id);
        await manager.PublishAsync(e).ConfigureAwait(false);
    }

    private async Task PublishApprovedEvent(IBehlogManager manager)
    {
        var e = new CommentApprovedEvent(Id);
        await manager.PublishAsync(e).ConfigureAwait(false); 
    }

    private async Task PublishBlockedEvent(IBehlogManager manager)
    {
        var e = new CommentBlockedEvent(Id);
        await manager.PublishAsync(e).ConfigureAwait(false);
    }

    private async Task PublishRejectedEvent(IBehlogManager manager)
    {
        var e = new CommentRejectedEvent(Id);
        await manager.PublishAsync(e).ConfigureAwait(false);
    }

    private async Task PublishSoftDeletedEvent(IBehlogManager manager)
    {
        var e = new CommentSoftDeletedEvent(Id);
        await manager.PublishAsync(e).ConfigureAwait(false);
    }

    private async Task PublishSpammedEvent(IBehlogManager manager)
    {
        var e = new CommentSpammedEvent(Id);
        await manager.PublishAsync(e).ConfigureAwait(false);
    }

    private void ChangeStatus(CommentStatus status)
    {
        Status = status;
        LastStatusChangedOn = DateTime.UtcNow;
        LastUpdatedByUserId = ""; //TODO : from userContext
        LastUpdatedByIp = ""; //TODO : from HttpContext
    }
    
    #endregion

}