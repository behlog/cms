using Behlog.Core;

namespace Behlog.Cms.Commands;

public class SoftDeleteWebsiteCommand : IBehlogCommand
{
    public SoftDeleteWebsiteCommand(Guid websiteId)
    {
        Id = websiteId;
    }
    
    public Guid Id { get; }
}