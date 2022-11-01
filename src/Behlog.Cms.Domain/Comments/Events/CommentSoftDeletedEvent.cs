using Behlog.Core.Domain;

namespace Behlog.Cms.Domain.Events;

public class CommentSoftDeletedEvent : BehlogDomainEvent
{
    public CommentSoftDeletedEvent(Guid commentId)
    {
        Id = commentId;
    }
    
    public Guid Id { get; }
}