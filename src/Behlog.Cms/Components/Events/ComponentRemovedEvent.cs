using Behlog.Core.Domain;

namespace Behlog.Cms.Events;


public class ComponentRemovedEvent : BehlogDomainEvent
{
    public ComponentRemovedEvent(Guid componentId)
    {
        Id = componentId;
    }
    
    public Guid Id { get; }
}