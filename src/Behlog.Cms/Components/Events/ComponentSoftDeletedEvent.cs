using Behlog.Core.Domain;

namespace Behlog.Cms.Events;


public class ComponentSoftDeletedEvent : BehlogDomainEvent
{
    public ComponentSoftDeletedEvent(Guid componentId)
    {
        Id = componentId;
    }
    
    public Guid Id { get; }
}