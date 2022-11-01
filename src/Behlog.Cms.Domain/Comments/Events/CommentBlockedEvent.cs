using Behlog.Core.Domain;

namespace Behlog.Cms.Domain.Events;

public class CommentBlockedEvent : BehlogDomainEvent
{
    public CommentBlockedEvent(Guid commentId)
    {
        Id = commentId;
    }
    
    public Guid Id { get; }
}