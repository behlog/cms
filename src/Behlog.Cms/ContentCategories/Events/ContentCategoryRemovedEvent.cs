using Behlog.Core;

namespace Behlog.Cms.Events;

public class ContentCategoryRemovedEvent : IBehlogEvent
{
    public ContentCategoryRemovedEvent(Guid categoryId)
    {
        Id = categoryId;
    }
    
    public Guid Id { get; }
}