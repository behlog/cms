using Behlog.Core;
using Behlog.Extensions;
using Behlog.Cms.Domain;
using Behlog.Core.Models;

namespace Behlog.Cms.Commands;


public class CreateComponentFilesCommand : IBehlogCommand<CommandResult>
{
    
    public CreateComponentFilesCommand(Guid componentId)
    {
        componentId.ThrowIfGuidIsEmpty(new BehlogInvalidEntityIdException(nameof(Component)));
        ComponentId = componentId;
        Files = new List<ComponentFileCommand>();
    }
    
    public Guid ComponentId { get; }
    public ICollection<ComponentFileCommand> Files { get; set; }
}