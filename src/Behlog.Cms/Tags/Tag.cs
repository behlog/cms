namespace Behlog.Cms.Domain;


public class Tag : AggregateRoot<Guid>
{
    private Tag() { }
    

    #region props

    public string Title { get; protected set; }
    public string Slug { get; protected set; }
    public Guid LangId { get; protected set; }
    public EntityStatusEnum Status { get; protected set; }
    public DateTime CreatedDate { get; protected set; }
    public string? CreatedByUserId { get; protected set; }
    public string? CreatedByIp { get; protected set; }

    #endregion

    #region Navigations

    public Language Language { get; protected set; }
    
    #endregion

    #region Builders

    public static Tag Create(
        CreateTagCommand command, IBehlogApplicationContext appContext, 
        IIdyfaUserContext userContext, ISystemDateTime dateTime)
    {
        command.ThrowExceptionIfArgumentIsNull(nameof(command));
        appContext.ThrowExceptionIfArgumentIsNull(nameof(appContext));
        userContext.ThrowExceptionIfArgumentIsNull(nameof(userContext));
        dateTime.ThrowExceptionIfArgumentIsNull(nameof(dateTime));
        
        var tag = new Tag
        {
            Id = Guid.NewGuid(),
            Title = command.Title?.CorrectYeKe().Trim()!,
            Status = EntityStatusEnum.Enabled,
            CreatedDate = dateTime.UtcNow,
            CreatedByUserId = userContext.UserId,
            CreatedByIp = appContext.IpAddress,
            LangId = command.LangId,
            Slug = command.Title?.MakeSlug()!
        };

        tag.Enqueue(new TagCreatedEvent(
            tag.Id, tag.Title!, tag.Slug, tag.LangId));
        
        return tag;
    }
    
    public void SoftDelete(SoftDeleteTagCommand command)
    {
        command.ThrowExceptionIfArgumentIsNull(nameof(command));
        
        Status = EntityStatusEnum.Deleted;
        Enqueue(new TagSoftDeletedEvent(Id, Title, Slug));
    }
    
    public void Remove()
    {
        Enqueue(new TagRemovedEvent(Id, Title));
    }
    
    #endregion
}