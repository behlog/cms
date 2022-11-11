using Behlog.Core;

namespace Behlog.Cms.Commands;

public class ApproveCommentCommand : IBehlogCommand
{
    public ApproveCommentCommand(Guid commentId)
    {
        Id = commentId;
    }
    
    public Guid Id { get; }
}