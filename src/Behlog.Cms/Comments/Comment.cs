namespace Behlog.Cms.Domain;

public class Comment : AggregateRoot<Guid>, IHasMetadata
{
    private Comment()
    {
    }

    #region Props

    public string? Title { get; protected set; }
    public string Body { get; protected set; }
    public ContentBodyType BodyType { get; protected set; }
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
    public string? CreatedByUserName { get; protected set; }
    public string? CreatedByUserDisplayName { get; protected set; }
    public string? LastUpdatedByUserId { get; protected set; }
    public string? LastUpdatedByUserName { get; protected set; }
    public string? LastUpdatedByUserDisplayName { get; protected set; }
    public string? CreatedByIp { get; protected set; }
    public string? LastUpdatedByIp { get; protected set; }
    #endregion

    #region Navigations

    public Content Content { get; protected set; }

    #endregion

    #region Builders

    public static Comment Create(
        CreateCommentCommand command, IBehlogApplicationContext appContext,
        IIdyfaUserContext userContext, ISystemDateTime dateTime)
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
            CreatedDate = dateTime.UtcNow,
            WebUrl = command.WebUrl?.Trim().CorrectYeKe()!,
            AuthorUserId = userContext.UserId,
            CreatedByIp = appContext.IpAddress,
            CreatedByUserId = userContext.UserId
        };
        
        comment.AddCreatedEvent();
        return comment;
    }


    public void Update(
        UpdateCommentCommand command, IBehlogApplicationContext appContext,
        IIdyfaUserContext userContext, ISystemDateTime dateTime)
    {
        command.ThrowExceptionIfArgumentIsNull(nameof(command));

        Title = command.Title?.Trim().CorrectYeKe()!;
        Body = command.Body?.CorrectYeKe()!;
        BodyType = command.BodyType;
        Email = command.Email?.Trim().CorrectYeKe()!;
        WebUrl = command.WebUrl?.Trim().CorrectYeKe()!;
        LastUpdated = dateTime.UtcNow;
        LastUpdatedByIp = appContext.IpAddress;
        LastUpdatedByUserId = userContext.UserId;

        AddUpdatedEvent();
    }
    
    public void SoftDelete(
        IBehlogApplicationContext appContext, 
        IIdyfaUserContext userContext, ISystemDateTime dateTime)
    {
        //TODO : Check if comment can be soft deleted
        if (Status == CommentStatusEnum.Deleted)
        {
            throw new BehlogAlreadySoftDeletedException();
        }
        
        ChangeStatus(CommentStatusEnum.Deleted, appContext, userContext, dateTime);

        AddSoftDeletedEvent();
    }

    public void Remove()
    {
        AddRemovedEvent();
    }
    

    /// <summary>
    /// Approve the <see cref="Comment"/>
    /// </summary>
    public void Approve(
        IBehlogApplicationContext appContext, 
        IIdyfaUserContext userContext, ISystemDateTime dateTime)
    {
        if(Status == CommentStatusEnum.Approved) return;
        
        ChangeStatus(CommentStatusEnum.Approved, appContext, userContext, dateTime);
        AddApprovedEvent();
    }

    /// <summary>
    /// Reject the <see cref="Comment"/>
    /// </summary>
    public void Reject(
        IBehlogApplicationContext appContext, 
        IIdyfaUserContext userContext, ISystemDateTime dateTime)
    {
        if(Status == CommentStatusEnum.Rejected) return;
        
        ChangeStatus(CommentStatusEnum.Rejected, appContext, userContext, dateTime);
        AddRejectedEvent();
    }


    public void Block(
        IBehlogApplicationContext appContext, 
        IIdyfaUserContext userContext, ISystemDateTime dateTime)
    {
        if(Status == CommentStatusEnum.Blocked) return;
        
        ChangeStatus(CommentStatusEnum.Blocked, appContext, userContext, dateTime);
        AddBlockedEvent();
    }


    public void MarkAsSpam(
        IBehlogApplicationContext appContext, 
        IIdyfaUserContext userContext, ISystemDateTime dateTime)
    {
        if(Status == CommentStatusEnum.Spam) return;
        
        ChangeStatus(CommentStatusEnum.Spam, appContext, userContext, dateTime);
        AddSpammedEvent();
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
    
    private void AddRemovedEvent()
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
    
    private void ChangeStatus(
        CommentStatusEnum status, IBehlogApplicationContext appContext, 
        IIdyfaUserContext userContext, ISystemDateTime dateTime)
    {
        Status = status;
        LastStatusChangedOn = dateTime.UtcNow;
        LastUpdatedByUserId = userContext.UserId;
        LastUpdatedByIp = appContext.IpAddress;
    }
    
    #endregion

}