using Behlog.Core;

namespace Behlog.Cms.Commands;


public class RemoveComponentCommand : IBehlogCommand
{
    public RemoveComponentCommand(Guid componentId)
    {
        Id = componentId;
    }
    
    public Guid Id { get; }
}