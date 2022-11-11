using Behlog.Core;

namespace Behlog.Cms.Commands;

public class RemoveCommentCommand : IBehlogCommand
{
    public RemoveCommentCommand(Guid commentId)
    {
        Id = commentId;
    }
    
    public Guid Id { get; }
}