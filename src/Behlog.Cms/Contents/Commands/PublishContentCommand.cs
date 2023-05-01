namespace Behlog.Cms.Commands;

public class PublishContentCommand : IBehlogCommand<CommandResult>
{
    public PublishContentCommand(Guid contentId)
    {
        Id = contentId;
    }
    
    public Guid Id { get; }
}