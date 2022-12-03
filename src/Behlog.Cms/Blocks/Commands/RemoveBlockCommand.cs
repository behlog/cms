using Behlog.Core;

namespace Behlog.Cms.Commands;

public class RemoveBlockCommand : IBehlogCommand
{

    public RemoveBlockCommand(Guid id)
    {
        Id = id;
    }
    
    public Guid Id { get; }
}