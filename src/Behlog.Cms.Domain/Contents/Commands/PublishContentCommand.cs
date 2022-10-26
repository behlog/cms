using Behlog.Core;

namespace Behlog.Cms.Commands;

public class PublishContentCommand : IBehlogCommand
{
    public PublishContentCommand(Guid contentId)
    {
        Id = contentId;
    }
    
    public Guid Id { get; }
}