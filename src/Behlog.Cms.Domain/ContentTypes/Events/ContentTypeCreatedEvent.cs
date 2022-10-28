using Behlog.Core;
using Behlog.Core.Domain;

namespace Behlog.Cms.Events;

public class ContentTypeCreatedEvent : BehlogDomainEvent
{
    public ContentTypeCreatedEvent(
        Guid id, string systemName, string title, string slug,
        string description)
    {
        Id = id;
        SystemName = systemName;
        Title = title;
        Slug = slug;
        Description = description;
    }
    
    public Guid Id { get; }
    public string SystemName { get; }
    public string Title { get; }
    public string Slug { get; }
    public string Description { get; }
}