using Behlog.Cms.Domain;
using Behlog.Core;

namespace Behlog.Cms.Commands;

public class SetWebsiteStatusCommand : IBehlogCommand
{
    public SetWebsiteStatusCommand(Guid websiteId, WebsiteStatus status)
    {
        Id = websiteId;
        Status = status;
    }
    
    public Guid Id { get; }
    
    public WebsiteStatus Status { get; }
}