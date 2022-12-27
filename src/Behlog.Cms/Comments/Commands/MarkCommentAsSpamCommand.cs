namespace Behlog.Cms.Commands;


public class MarkCommentAsSpamCommand : IBehlogCommand
{

    public MarkCommentAsSpamCommand(Guid commentId)
    {
        Id = commentId;
    }
    
    public Guid Id { get; }
}