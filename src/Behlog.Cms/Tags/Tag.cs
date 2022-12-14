using Behlog.Core;
using Behlog.Cms.Events;
using Behlog.Extensions;
using Behlog.Core.Domain;
using Behlog.Cms.Commands;

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

    public static Tag Create(CreateTagCommand command)
    {
        command.ThrowExceptionIfArgumentIsNull(nameof(command));

        var tag = new Tag();
        tag.Id = Guid.NewGuid();
        tag.Title = command.Title?.CorrectYeKe().Trim()!;
        tag.Slug = tag.Title?.MakeSlug()!;
        tag.Status = EntityStatusEnum.Enabled;
        tag.CreatedDate = DateTime.UtcNow;
        tag.CreatedByUserId = null; //read from UserContext
        tag.CreatedByIp = null; //TODO : read from HttpContext
        tag.LangId = command.LangId;
        
        //TODO : add CreatedEvent
        
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