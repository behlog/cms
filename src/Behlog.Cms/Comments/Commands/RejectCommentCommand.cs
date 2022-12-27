namespace Behlog.Cms.Commands;

public class RejectCommentCommand : IBehlogCommand
{
    public RejectCommentCommand(Guid commentId)
    {
        Id = commentId;
    }
    
    public Guid Id { get; }
}