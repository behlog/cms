using iman.Domain;

namespace Behlog.Cms.Events;

public class ContentRemovedEvent : DomainEvent
{
    public ContentRemovedEvent(Guid id) => Id = id;

    public Guid Id { get; }
}