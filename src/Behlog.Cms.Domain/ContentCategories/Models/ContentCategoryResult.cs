using Behlog.Core;

namespace Behlog.Cms.Models;

public class ContentCategoryResult
{
    public ContentCategoryResult(
        Guid id, string title, string altTitle, 
        string slug, Guid? parentId, Guid? contentTypeId, 
        EntityStatus status, string description)
    {
        Id = id;
        Title = title;
        AltTitle = altTitle;
        Slug = slug;
        Description = description;
        Status = status;
        ParentId = parentId;
        ContentTypeId = contentTypeId;
    }
    
    public Guid Id { get; protected set; }
    public string Title { get; protected set; }
    public string AltTitle { get; protected set; }
    public string Slug { get; protected set; }
    public Guid? ParentId { get; protected set; }
    public string Description { get; protected set; }
    public Guid? ContentTypeId { get; protected set; }
    public EntityStatus Status { get; protected set; }
}