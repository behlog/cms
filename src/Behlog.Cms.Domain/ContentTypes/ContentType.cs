using Behlog.Cms.Commands;
using Behlog.Cms.Domain;
using Behlog.Cms.Events;
using Behlog.Core.Domain;
using Behlog.Extensions;

namespace Behlog.Core;

public class ContentType : AggregateRoot<Guid> {
    
    private ContentType()
    {
        
    }

    #region Props
    
    public string SystemName { get; protected set; }
    public string Title { get; protected set; }
    public string Slug { get; protected set; }
    public string? Description { get; protected set; }
    public Guid LangId { get; protected set; }
    public string LangCode { get; protected set; }
    public EntityStatus Status { get; protected set; }
    public DateTime CreatedDate { get; protected set; }
    public DateTime? LastUpdated { get; protected set; }
    public DateTime? LastStatusChangedOn { get; protected set; }

    #endregion

    #region Navigations

    public Language Language { get; protected set; }

    #endregion
    
    #region Builders

    public static ContentType Create(CreateContentTypeCommand command)
    {
        command.ThrowExceptionIfArgumentIsNull(nameof(command));
        
        var contentType = new ContentType
        {
            Id = Guid.NewGuid(),
            SystemName = command.SystemName.Trim(),
            LangId = command.LangId,
            Description = command.Description?.CorrectYeKe()!,
            Slug = command.Slug?.Trim().CorrectYeKe()!,
            Status = EntityStatus.Enabled,
            Title = command.Title?.Trim().CorrectYeKe()!,
            CreatedDate = DateTime.UtcNow
        };

        contentType.AddCreatedEvent();
        return contentType;
    }


    public void Update(UpdateContentTypeCommand command)
    {
        command.ThrowExceptionIfArgumentIsNull(nameof(command));

        Title = command.Title?.Trim().CorrectYeKe()!;
        Slug = command.Slug?.Trim().MakeSlug().CorrectYeKe()!;
        SystemName = command.SystemName?.Trim()!;
        LangId = command.LangId;
        if (command.Enabled && Status != EntityStatus.Enabled ||
            !command.Enabled && Status != EntityStatus.Disabled)
        {
            LastStatusChangedOn = DateTime.UtcNow;
        }
        Status = command.Enabled ? EntityStatus.Enabled : EntityStatus.Disabled;
        Description = command.Description?.CorrectYeKe()!;
        LastUpdated = DateTime.UtcNow;
        
        AddUpdatedEvent();
    }


    public void SoftDelete()
    {
        ChangeStatus(EntityStatus.Deleted);
        var e = new ContentTypeSoftDeletedEvent(Id);
        Enqueue(e);
    }

    private void ChangeStatus(EntityStatus status)
    {
        Status = status;
        LastStatusChangedOn = DateTime.UtcNow;
    }
    
    #endregion

    #region Events

    private void AddCreatedEvent()
    {
        var e = new ContentTypeCreatedEvent(
            Id, SystemName, Title, LangId, Slug, Description);
        Enqueue(e);
    }

    private void AddUpdatedEvent()
    {
        var e = new ContentTypeUpdatedEvent(
            Id, SystemName, Title, LangId, Slug, Status,
            Description, LastStatusChangedOn);
        Enqueue(e);
    }

    #endregion
}