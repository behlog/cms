using Behlog.Core;

namespace Behlog.Cms.Models;

public class ContentCategoryResult
{
    public ContentCategoryResult(
        Guid id, string title, string altTitle, 
        string slug, Guid langId, string langCode,
        Guid? parentId, Guid? contentTypeId, 
        EntityStatus status, string description)
    {
        Id = id;
        Title = title;
        AltTitle = altTitle;
        Slug = slug;
        LangId = langId;
        LangCode = langCode;
        Description = description;
        Status = status;
        ParentId = parentId;
        ContentTypeId = contentTypeId;
    }
    
    public Guid Id { get; }
    public string Title { get; }
    public string AltTitle { get; }
    public string Slug { get; }
    public Guid LangId { get; }
    public string LangCode { get; }
    public Guid? ParentId { get; }
    public string Description { get; }
    public Guid? ContentTypeId { get; }
    public EntityStatus Status { get; }
}