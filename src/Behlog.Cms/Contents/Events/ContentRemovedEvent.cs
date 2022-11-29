using Behlog.Core.Domain;

namespace Behlog.Cms.Events;

public class ContentRemovedEvent : BehlogDomainEvent
{
    public ContentRemovedEvent(Guid id) => Id = id;

    public Guid Id { get; }
}