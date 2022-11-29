using Behlog.Core.Domain;

namespace Behlog.Cms.Events;

public class ContentSoftDeletedEvent : BehlogDomainEvent
{
    public ContentSoftDeletedEvent(Guid contentId)
    {
        ContentId = contentId;
    }
    
    public Guid ContentId { get; }
}