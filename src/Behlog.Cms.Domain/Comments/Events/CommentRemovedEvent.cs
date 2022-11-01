using Behlog.Core.Domain;

namespace Behlog.Cms.Domain.Events;

public class CommentRemovedEvent : BehlogDomainEvent
{
    public CommentRemovedEvent(Guid commentId)
    {
        Id = commentId;
    }
    
    public Guid Id { get; }
}