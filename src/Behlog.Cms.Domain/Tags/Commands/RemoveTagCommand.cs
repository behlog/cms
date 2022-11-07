using Behlog.Core;

namespace Behlog.Cms.Commands;

public class RemoveTagCommand : IBehlogCommand
{

    public RemoveTagCommand(Guid tagId, string title = "")
    {
        Id = Id;
        Title = title;
    }
    
    public Guid Id { get; }
    public string Title { get; }
}