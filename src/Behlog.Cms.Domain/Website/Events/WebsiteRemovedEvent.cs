using Behlog.Core;

namespace Behlog.Cms.Events;

public class WebsiteRemovedEvent : IBehlogEvent
{
    public WebsiteRemovedEvent(Guid websiteId)
    {
        Id = websiteId;
    }
    
    public Guid Id { get; }
}