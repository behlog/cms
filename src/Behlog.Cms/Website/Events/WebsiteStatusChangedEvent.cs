using Behlog.Core;
using Behlog.Cms.Domain;

namespace Behlog.Cms.Events;


public class WebsiteStatusChangedEvent : IBehlogEvent
{
    public WebsiteStatusChangedEvent(Guid websiteId, WebsiteStatus status)
    {
        Id = websiteId;
        Status = status;
    }
    
    public Guid Id { get; }
    
    public WebsiteStatus Status { get; }
}