namespace Behlog.Cms.Commands;


public class SoftDeleteCommentCommand : IBehlogCommand
{
    public SoftDeleteCommentCommand(Guid commentId)
    {
        Id = commentId;
    }
    
    public Guid Id { get; }
}