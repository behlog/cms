using Behlog.Cms.Models;
using Behlog.Core;
using Behlog.Core.Domain;

namespace Behlog.Cms.Events;


public class ComponentUpdatedEvent : BehlogDomainEvent
{
    public ComponentUpdatedEvent(
        Guid id, Guid websiteId, Guid langId, string name, string title, string componentType, string category,
        string? description, string? attributes, EntityStatus status, string? author, string? authorEmail, 
        Guid? parentId, bool isRtl, string? keywords, string? viewPath, DateTime createdDate, DateTime? lastUpdated,
        string? createdByUserId, string? createdByIp, string? lastUpdatedByUserId, string? lastUpdatedByIp, 
        IReadOnlyCollection<MetaResult> meta)
    {
        Id = id;
        WebsiteId = websiteId;
        LangId = langId;
        Name = name;
        Title = title;
        ComponentType = componentType;
        Category = category;
        Description = description;
        Attributes = attributes;
        Status = status;
        Author = author;
        AuthorEmail = authorEmail;
        ParentId = parentId;
        IsRtl = isRtl;
        Keywords = keywords;
        ViewPath = viewPath;
        CreatedDate = createdDate;
        LastUpdated = lastUpdated;
        CreatedByUserId = createdByUserId;
        CreatedByIp = createdByIp;
        LastUpdatedByUserId = lastUpdatedByUserId;
        LastUpdatedByIp = lastUpdatedByIp;
        Meta = meta;
    }
    
    public Guid Id { get; }
    public Guid WebsiteId { get; }
    public Guid LangId { get; }
    public string Name { get; }
    public string Title { get; }
    public string ComponentType { get; }
    public string Category { get; }
    public string? Description { get; }
    public string? Attributes { get; }
    public EntityStatus Status { get; }
    public string? Author { get; }
    public string? AuthorEmail { get; }
    public Guid? ParentId { get; }
    public bool IsRtl { get; }
    public string? Keywords { get; }
    public string? ViewPath { get; }
    public DateTime CreatedDate { get; }
    public DateTime? LastUpdated { get; }
    public string? CreatedByUserId { get; }
    public string? CreatedByIp { get; }
    public string? LastUpdatedByUserId { get; }
    public string? LastUpdatedByIp { get; }
    public IReadOnlyCollection<MetaResult> Meta { get; }
}