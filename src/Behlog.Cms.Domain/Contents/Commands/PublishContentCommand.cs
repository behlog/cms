using Behlog.Core;

namespace Behlog.Cms.Commands;

public class PublishContentCommand : IBehlogCommand<BehlogResult>
{
    public PublishContentCommand(Guid contentId)
    {
        Id = contentId;
    }
    
    public Guid Id { get; }
}