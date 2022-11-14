using Behlog.Core;

namespace Behlog.Cms.Commands;

public class RemoveWebsiteCommand : IBehlogCommand
{
    public RemoveWebsiteCommand(Guid websiteId)
    {
        Id = websiteId;
    }

    public Guid Id { get; }
}