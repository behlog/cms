using Behlog.Cms.Domain;
using Behlog.Core;
using Behlog.Extensions;

namespace Behlog.Cms.Models;

public class ComponentResult
{
    private ComponentResult(
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

    #region props

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

    #endregion


    public static ComponentResult Create(
        Guid id, Guid websiteId, string name, string title, string category,
        string? description, EntityStatus status, string? author, string? authorEmail,
        Guid? parentId, DateTime createdDate, DateTime? lastUpdated)
    {
        return new ComponentResult(
            id, websiteId, name, title, category,
            description, status, author, authorEmail,
            parentId, createdDate, lastUpdated);
    }


    public static ComponentResult Create(Component component)
    {
        component.ThrowExceptionIfArgumentIsNull(nameof(component));

        return new ComponentResult(
            component.Id, component.WebsiteId, component.Name, component.Title, component.Category,
            component.Description, component.Status, component.Author, component.AuthorEmail,
            component.ParentId, component.CreatedDate, component.LastUpdated
        );
    }
}