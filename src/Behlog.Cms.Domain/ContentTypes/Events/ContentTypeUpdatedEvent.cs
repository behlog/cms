using Behlog.Core;
using Behlog.Core.Domain;

namespace Behlog.Cms.Events;

public class ContentTypeUpdatedEvent : BehlogDomainEvent
{
    public ContentTypeUpdatedEvent(
        Guid id, string systemName, string title, 
        Guid langId, string slug, EntityStatus status,
        string description, DateTime? lastStatusChangedOn)
    {
        Id = id;
        SystemName = systemName;
        Title = title;
        LangId = langId;
        Slug = slug;
        Description = description;
        Status = status;
        LastStatusChangedOn = lastStatusChangedOn;
    }   
    
    public Guid Id { get; }
    public string SystemName { get; }
    public string Title { get; }
    public Guid LangId { get; }
    public string Slug { get; }
    public string Description { get; }
    public EntityStatus Status { get; }
    public DateTime? LastStatusChangedOn { get; }
}