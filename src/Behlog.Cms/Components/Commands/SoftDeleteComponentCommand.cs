using Behlog.Core;

namespace Behlog.Cms.Commands;


public class SoftDeleteComponentCommand : IBehlogCommand
{
    public SoftDeleteComponentCommand(Guid componentId)
    {
        Id = componentId;
    }
    
    public Guid Id { get; }
}