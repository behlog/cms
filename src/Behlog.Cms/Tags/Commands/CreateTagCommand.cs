namespace Behlog.Cms.Commands;

public class CreateTagCommand : IBehlogCommand<CommandResult<TagResult>>
{

    public CreateTagCommand(string title, Guid langId)
    {
        Title = title;
        LangId = langId;
    }
    
    public string Title { get; }
    public Guid LangId { get; }
    
}