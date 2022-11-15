using Behlog.Core;
using Behlog.Core.Validations;

namespace Behlog.Cms.Commands;

public class PublishContentCommand : IBehlogCommand<ValidationResult>
{
    public PublishContentCommand(Guid contentId)
    {
        Id = contentId;
    }
    
    public Guid Id { get; }
}