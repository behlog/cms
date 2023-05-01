namespace Behlog.Cms.Commands;

public class RemoveContentCommand : IBehlogCommand
{
    public RemoveContentCommand(Guid contentId)
    {
        contentId.ThrowIfGuidIsEmpty(
            new BehlogInvalidEntityIdException(nameof(Content)));
        Id = contentId;
    }
    
    public Guid Id { get; }
}