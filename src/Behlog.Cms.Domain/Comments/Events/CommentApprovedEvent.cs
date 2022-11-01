using Behlog.Core.Domain;

namespace Behlog.Cms.Domain.Events;

public class CommentApprovedEvent : BehlogDomainEvent
{
    public CommentApprovedEvent(Guid commentId)
    {
        Id = commentId;
    }
    
    public Guid Id { get; }
}