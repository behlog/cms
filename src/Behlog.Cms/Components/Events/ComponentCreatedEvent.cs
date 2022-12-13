using Behlog.Core;

namespace Behlog.Cms.Events;


public class ComponentCreatedEvent
{
    public ComponentCreatedEvent(
        Guid id, Guid websiteId, string name, string title, string category,
        string? description, EntityStatus status, string? author, string? authorEmail, 
        Guid? parentId, DateTime createdDate, DateTime? lastUpdated)
    {
        Id = id;
        WebsiteId = websiteId;
        Name = name;
        Title = title;
        Category = category;
        Description = description;
        Status = status;
        Author = author;
        AuthorEmail = authorEmail;
        ParentId = parentId;
        CreatedDate = createdDate;
        LastUpdated = lastUpdated;
    }
    
    public Guid Id { get; }
    public Guid WebsiteId { get; }
    public string Name { get; }
    public string Title { get; }
    public string Category { get; }
    public string? Description { get; }
    public EntityStatus Status { get; }
    public string? Author { get; }
    public string? AuthorEmail { get; }
    public Guid? ParentId { get; }
    public DateTime CreatedDate { get; }
    public DateTime? LastUpdated { get; }
}