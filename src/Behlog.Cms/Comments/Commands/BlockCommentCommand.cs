namespace Behlog.Cms.Commands;


public class BlockCommentCommand : IBehlogCommand
{

    public BlockCommentCommand(Guid commentId)
    {
        Id = commentId;
    }
    
    public Guid Id { get; }
}