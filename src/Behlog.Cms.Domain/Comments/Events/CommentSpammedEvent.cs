using Behlog.Core.Domain;

namespace Behlog.Cms.Domain.Events;

public class CommentSpammedEvent : BehlogDomainEvent
{
    public CommentSpammedEvent(Guid commentId)
    {
        Id = commentId;
    }
    
    public Guid Id { get; }
}