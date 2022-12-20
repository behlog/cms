using Behlog.Core;
using Behlog.Core.Models;

namespace Behlog.Cms.Commands;


public class SoftDeleteComponentCommand : IBehlogCommand<CommandResult>
{
    public SoftDeleteComponentCommand(Guid componentId)
    {
        Id = componentId;
    }
    
    public Guid Id { get; }
}