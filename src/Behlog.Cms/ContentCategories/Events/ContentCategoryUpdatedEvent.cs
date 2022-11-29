using Behlog.Core;
using Behlog.Core.Domain;

namespace Behlog.Cms.Events;

public class ContentCategoryUpdatedEvent : BehlogDomainEvent
{

    public ContentCategoryUpdatedEvent(
        Guid id,
        string title,
        string altTitle,
        string slug,
        Guid? parentId,
        string description,
        Guid? contentTypeId,
        EntityStatus status
    ) : base()
    {
        Id = id;
        Title = title;
        AltTitle = altTitle;;
        Slug = slug;
        ParentId = parentId;
        Description = description;
        ContentTypeId = contentTypeId;
        Status = status;
    }

    public Guid Id { get; }
    public string Title { get; }
    public string AltTitle { get; }
    public string Slug { get; }
    public Guid? ParentId { get; }
    public string Description { get; }
    public Guid? ContentTypeId { get; }
    public EntityStatus Status { get; }
}