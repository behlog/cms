using Behlog.Core;
using Behlog.Core.Domain;

namespace Behlog.Cms.Events;

public class ContentTypeUpdatedEvent : BehlogDomainEvent
{
    public ContentTypeUpdatedEvent(
        Guid id, string systemName, string title, 
        string lang, string slug, EntityStatus status,
        string description, DateTime? lastStatusChangedOn)
    {
        Id = id;
        SystemName = systemName;
        Title = title;
        Lang = lang;
        Slug = slug;
        Description = description;
        Status = status;
        LastStatusChangedOn = lastStatusChangedOn;
    }   
    
    public Guid Id { get; }
    public string SystemName { get; }
    public string Title { get; }
    public string Lang { get; }
    public string Slug { get; }
    public string Description { get; }
    public EntityStatus Status { get; }
    public DateTime? LastStatusChangedOn { get; }
}