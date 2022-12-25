using Behlog.Cms.Domain;
using Behlog.Core;
using Behlog.Extensions;

namespace Behlog.Cms.Models;


public class ComponentResult
{
    private ComponentResult(
        Guid id, Guid websiteId, Guid langId, string name, string title, string componentType, string category,
        string? attributes, string? description, EntityStatusEnum status, string? author, string? authorEmail, 
        Guid? parentId, bool isRtl, string? keywords, string? viewPath, DateTime createdDate, DateTime? lastUpdated,
        string? createdByUserId, string? createdByIp, string? lastUpdatedByUserId, string? lastUpdatedByIp
        )
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
    }

    #region props

    public Guid Id { get; }
    public Guid WebsiteId { get; }
    public Guid LangId { get; }
    public string Name { get; }
    public string Title { get; }
    public string ComponentType { get; }
    public string Category { get; }
    public string? Description { get; }
    public string? Attributes { get; }
    public EntityStatusEnum Status { get; }
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
    
    public IReadOnlyCollection<MetaResult> Meta { get; set; }
    
    public IReadOnlyCollection<ComponentFileResult> Files { get; set; }
    
    public string LangCode { get; set; }
    public string LangTitle { get; set; }
    #endregion


    public static ComponentResult Create(
        Guid id, Guid websiteId, Guid langId, string name, string title, string componentType, string category,
        string? attributes, string? description, EntityStatusEnum status, string? author, string? authorEmail, 
        Guid? parentId, bool isRtl, string? keywords, string? viewPath, DateTime createdDate, DateTime? lastUpdated,
        string? createdByUserId, string? createdByIp, string? lastUpdatedByUserId, string? lastUpdatedByIp)
    {
        return new ComponentResult(
            id, websiteId, langId, name, title, componentType, category,
            attributes, description, status, author, authorEmail,
            parentId, isRtl, keywords, viewPath, createdDate, lastUpdated,
            createdByUserId, createdByIp, lastUpdatedByUserId, lastUpdatedByIp);
    }


    public static ComponentResult Create(Component component)
    {
        component.ThrowExceptionIfArgumentIsNull(nameof(component));

        return new ComponentResult(
            component.Id, component.WebsiteId, component.LangId, component.Name, component.Title, 
            component.ComponentType, component.Category, component.Attributes, component.Description, 
            component.Status, component.Author, component.AuthorEmail, component.ParentId, component.IsRtl,
            component.Keywords, component.ViewPath, component.CreatedDate, component.LastUpdated,
            component.CreatedByUserId, component.CreatedByIp, component.LastUpdatedByUserId, component.LastUpdatedByIp
        );
    }

    public ComponentResult WithMeta(IReadOnlyCollection<MetaResult> meta)
    {
        Meta = meta;
        return this;
    }


    public ComponentResult WithMeta(IReadOnlyCollection<ComponentMeta> meta)
    {
        Meta = meta?.Select(_ => _.ToResult()).ToList()!;
        return this;
    }
    
    public ComponentResult WithLanguage(Language? language)
    {
        if (language is null) return this;
        
        LangCode = language.Code;
        LangTitle = language.Title;
        return this;
    }

    public ComponentResult WithFiles(IReadOnlyCollection<ComponentFile> files)
    {
        var result = new List<ComponentFileResult>();
        foreach (var file in files)
        {
            result.Add(new ComponentFileResult(
                file.ComponentId, file.FileId, file.Title, file.FileName, file.Description)
                .WithFile(file.File));
        }
        
        Files = result.ToList();
        return this;
    }


    public ComponentResult WithFiles(IReadOnlyCollection<ComponentFileResult> files)
    {
        Files = files;
        return this;
    }
}