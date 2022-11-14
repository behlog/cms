using Behlog.Core;

namespace Behlog.Cms.Events;

public class WebsiteSoftDeletedEvent : IBehlogEvent
{
    public WebsiteSoftDeletedEvent(Guid websiteId)
    {
        Id = websiteId;
    }
    
    public Guid Id { get; }
}