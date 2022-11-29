using Behlog.Core.Domain;

namespace Behlog.Cms.Events;

public class CommentApprovedEvent : BehlogDomainEvent
{
    public CommentApprovedEvent(Guid commentId)
    {
        Id = commentId;
    }
    
    public Guid Id { get; }
}