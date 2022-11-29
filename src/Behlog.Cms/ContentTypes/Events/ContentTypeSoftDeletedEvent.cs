using Behlog.Core.Domain;

namespace Behlog.Cms.Events;

public class ContentTypeSoftDeletedEvent : BehlogDomainEvent
{

    public ContentTypeSoftDeletedEvent(Guid contentTypeId)
    {
        Id = contentTypeId;
    }
    
    public Guid Id { get; }
}