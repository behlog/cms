using Behlog.Core;
using Behlog.Core.Models;

namespace Behlog.Cms.Commands;


public class RemoveComponentCommand : IBehlogCommand<CommandResult>
{
    public RemoveComponentCommand(Guid componentId)
    {
        Id = componentId;
    }
    
    public Guid Id { get; }
}