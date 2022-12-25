using Behlog.Core;
using Behlog.Extensions;
using Behlog.Cms.Events;
using Behlog.Core.Domain;
using Behlog.Cms.Commands;
using Behlog.Core.Contracts;
using Idyfa.Core.Contracts;

namespace Behlog.Cms.Domain;

public class Comment : AggregateRoot<Guid>, IHasMetadata
{
    private Comment()
    {
    }

    #region Props

    public string? Title { get; protected set; }
    public string Body { get; protected set; }
    public ContentBodyTypeEnum BodyType { get; protected set; }
    public CommentStatusEnum Status { get; protected set; }
    public string? Email { get; protected set; }
    public string? WebUrl { get; protected set; }
    public string? AuthorUserId { get; protected set; }
    public string? AuthorName { get; protected set; }
    public Guid ContentId { get; protected set; }
    public DateTime CreatedDate { get; protected set; }
    public DateTime? LastUpdated { get; protected set; }
    public DateTime? LastStatusChangedOn { get; protected set; }
    public string? CreatedByUserId { get; protected set; }
    public string? LastUpdatedByUserId { get; protected set; }
    public string? CreatedByIp { get; protected set; }
    public string? LastUpdatedByIp { get; protected set; }
    #endregion

    #region Navigations

    public Content Content { get; protected set; }

    #endregion

    #region Builders

    public static Comment Create(CreateCommentCommand command)
    {
        command.ThrowExceptionIfArgumentIsNull(nameof(command));

        var comment = new Comment
        {
            Id = Guid.NewGuid(),
            Title = command.Title?.Trim().CorrectYeKe()!,
            Body = command.Body?.CorrectYeKe().Trim()!,
            Email = command.Email?.Trim()!,
            AuthorName = command.AuthorName?.Trim().CorrectYeKe()!,
            BodyType = command.BodyType,
            Status = CommentStatusEnum.Created,
            ContentId = command.ContentId,
            CreatedDate = DateTime.UtcNow,
            WebUrl = command.WebUrl?.Trim().CorrectYeKe()!,
            AuthorUserId = "", //TODO : read from UserContext
            CreatedByIp = "", //TODO : read from HttpContext
            CreatedByUserId = ""
        };
        
        comment.AddCreatedEvent();
        return comment;
    }


    public void Update(UpdateCommentCommand command)
    {
        command.ThrowExceptionIfArgumentIsNull(nameof(command));

        Title = command.Title?.Trim().CorrectYeKe()!;
        Body = command.Body?.CorrectYeKe()!;
        BodyType = command.BodyType;
        Email = command.Email?.Trim().CorrectYeKe()!;
        WebUrl = command.WebUrl?.Trim().CorrectYeKe()!;
        LastUpdated = DateTime.UtcNow;
        LastUpdatedByIp = ""; //TODO : read from Context
        LastUpdatedByUserId = ""; //TODO : read from context

        AddUpdatedEvent();
    }
    
    public void SoftDelete()
    {
        //TODO : Check if comment can be soft deleted
        ChangeStatus(CommentStatusEnum.Deleted);

        AddSoftDeletedEvent();
    }

    public void Remove()
    {
        AddRemovedEvent();
    }
    

    /// <summary>
    /// Approve the <see cref="Comment"/>
    /// </summary>
    public void Approve()
    {
        if(Status == CommentStatusEnum.Approved) return;
        
        ChangeStatus(CommentStatusEnum.Approved);
        AddApprovedEvent();
    }

    /// <summary>
    /// Reject the <see cref="Comment"/>
    /// </summary>
    /// <param name="manager"></param>
    public void Reject()
    {
        if(Status == CommentStatusEnum.Rejected) return;
        
        ChangeStatus(CommentStatusEnum.Rejected);
        AddRejectedEvent();
    }


    public void Block()
    {
        if(Status == CommentStatusEnum.Blocked) return;
        
        ChangeStatus(CommentStatusEnum.Blocked);
        AddBlockedEvent();
    }


    public void MarkAsSpam()
    {
        if(Status == CommentStatusEnum.Spam) return;
        
        ChangeStatus(CommentStatusEnum.Spam);
        AddSpammedEvent();
    }

    #endregion

    #region Methods

    public void SetIdentityOnAdd(
        IIdyfaUserContext userContext, IBehlogApplicationContext applicationContext)
    {
        CreatedByUserId = userContext.UserId;
        AuthorUserId = userContext.UserId;
        CreatedByIp = applicationContext.IpAddress;
    }

    public void SetIdentityOnUpdate(
        IIdyfaUserContext userContext, IBehlogApplicationContext applicationContext)
    {
        LastUpdatedByUserId = userContext.UserId;
        LastUpdatedByIp = applicationContext.IpAddress;
    }

    #endregion

    #region Event Publishers

    private void AddCreatedEvent()
    {
        var e = new CommentCreatedEvent(
            Id, Title, Body, BodyType, Email, WebUrl, AuthorUserId,
            AuthorName, CreatedByUserId, CreatedByIp, CreatedDate);
        Enqueue(e);
    }


    private void AddUpdatedEvent()
    {
        var e = new CommentUpdatedEvent(
            Id, Title, Body, BodyType, Email, WebUrl, AuthorUserId,
            AuthorName, CreatedByUserId, LastUpdatedByUserId,
            CreatedByIp, LastUpdatedByIp, CreatedDate, LastUpdated);
        Enqueue(e);
    }
    
    public void AddRemovedEvent()
    {
        var e = new CommentRemovedEvent(Id);
        Enqueue(e);
    }

    private void AddApprovedEvent()
    {
        var e = new CommentApprovedEvent(Id);
        Enqueue(e);
    }
    
    private void AddBlockedEvent()
    {
        var e = new CommentBlockedEvent(Id);
        Enqueue(e);
    }

    private void AddRejectedEvent()
    {
        var e = new CommentRejectedEvent(Id);
        Enqueue(e);
    }

    private void AddSoftDeletedEvent()
    {
        var e = new CommentSoftDeletedEvent(Id);
        Enqueue(e);
    }

    private void AddSpammedEvent()
    {
        var e = new CommentSpammedEvent(Id);
        Enqueue(e);
    }
    
    private void ChangeStatus(CommentStatusEnum status)
    {
        Status = status;
        LastStatusChangedOn = DateTime.UtcNow;
        LastUpdatedByUserId = ""; //TODO : from userContext
        LastUpdatedByIp = ""; //TODO : from HttpContext
    }
    
    #endregion

}