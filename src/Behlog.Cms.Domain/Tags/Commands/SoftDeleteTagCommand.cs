using Behlog.Core;

namespace Behlog.Cms.Commands;

public class SoftDeleteTagCommand : IBehlogCommand
{
    public SoftDeleteTagCommand(Guid tagId)
    {
        Id = tagId;
    }
    
    public Guid Id { get; }
}