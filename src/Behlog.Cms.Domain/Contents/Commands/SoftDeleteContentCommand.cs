using Behlog.Core;

namespace Behlog.Cms.Commands;

public class SoftDeleteContentCommand : IBehlogCommand
{

    public SoftDeleteContentCommand(Guid contentId)
    {
        Id = contentId;
    }
    
    public Guid Id { get; }
}