using Behlog.Core.Domain;

namespace Behlog.Cms.Events;

public class CommentRejectedEvent : BehlogDomainEvent
{
    public CommentRejectedEvent(Guid commentId)
    {
        Id = commentId;
    }
    
    public Guid Id { get; }
}