using Behlog.Core;

namespace Behlog.Cms.Commands;

public class SoftDeleteBlockCommand : IBehlogCommand
{
    public SoftDeleteBlockCommand(Guid id)
    {
        Id = id;
    }
    
    public Guid Id { get; }
}