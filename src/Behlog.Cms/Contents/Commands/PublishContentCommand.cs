using Behlog.Core;
using Behlog.Core.Models;
using Behlog.Core.Validations;

namespace Behlog.Cms.Commands;

public class PublishContentCommand : IBehlogCommand<CommandResult>
{
    public PublishContentCommand(Guid contentId)
    {
        Id = contentId;
    }
    
    public Guid Id { get; }
}