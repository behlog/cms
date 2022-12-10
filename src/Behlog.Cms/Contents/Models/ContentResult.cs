using Behlog.Core;
using Behlog.Cms.Domain;

namespace Behlog.Cms.Models;


public class ContentResult
{

    public ContentResult(
        Guid id, string title, string slug, Guid contentTypeId, Guid langId, string body,
        ContentBodyType bodyType, string authorUserId, string summary, ContentStatus status, string altTitle,
        int orderNum, DateTime? lastStatusChangedDate, DateTime? publishDate, string? iconName, string? viewPath, 
        DateTime createdDate, DateTime? lastUpdated, string? createdByUserId, string? lastUpdatedByUserId, 
        string? createdByIp, string? lastUpdatedByIp)
    {
        Id = id;
        Title = title;
        Slug = slug;;
        ContentTypeId = contentTypeId;
        LangId = langId;
        Body = body;
        BodyType = bodyType;
        AuthorUserId = authorUserId;
        Summary = summary;
        Status = status;
        AltTitle = altTitle;
        OrderNum = orderNum;
        LastStatusChangedDate = lastStatusChangedDate;
        PublishDate = publishDate;
        IconName = iconName;
        ViewPath = viewPath;
        CreatedDate = createdDate;
        LastUpdated = lastUpdated;
        CreatedByUserId = createdByUserId;
        LastUpdatedByUserId = lastUpdatedByUserId;
        CreatedByIp = createdByIp;
        LastUpdatedByIp = lastUpdatedByIp;
    }


    public ContentResult WithLanguage(Language language)
    {
        if (language is null) return this;
        
        LangCode = language.Code;
        LangTitle = language.Title;
        return this;
    }
    
    public ContentResult WithFiles(IReadOnlyCollection<ContentFileResult> files)
    {
        Files = files;
        return this;
    }

    public ContentResult WithFiles(IReadOnlyCollection<ContentFile> files)
    {
        var result = new List<ContentFileResult>();
        foreach(var file in files)
            result.Add(new ContentFileResult(
                file.ContentId, file.FileId, file.Title, file.FileName, file.Description)
                .WithFile(file.File));
        Files = result.ToList();
        return this;
    }

    public ContentResult WithMeta(IReadOnlyCollection<MetaResult> meta)
    {
        Meta = meta;
        return this;
    }

    public ContentResult WithMeta(IReadOnlyCollection<ContentMeta> meta)
    {
        var result = new List<MetaResult>();
        foreach(var m in meta)
            result.Add(m.ToResult());
        Meta = result.ToList();
        
        return this;
    }

    public ContentResult WithContentType(ContentType contentType)
    {
        if (contentType is null) return this;
        
        ContentTypeSlug = contentType.Slug;
        ContentTypeTitle = contentType.Title;
        return this;
    }

    public ContentResult WithCategories(
        IReadOnlyCollection<ContentCategoryItem> categoryItems)
    {
        if (categoryItems is null) return this;

        var result = new List<ContentCategoryItemResult>();
        foreach(var cat in categoryItems) 
            result.Add(ContentCategoryItemResult.From(cat));
        Categories = result.ToList();
        return this;
    }

    public ContentResult WithAuthor(string userName, string displayName)
    {
        AuthorUserName = userName;
        AuthorUserDisplayName = displayName;
        return this;
    }

    public ContentResult WithTags(IReadOnlyCollection<ContentTag> tags)
    {
        var result = new List<ContentTagResult>();
        foreach(var tag in tags)
            result.Add(new ContentTagResult(tag.ContentId, tag.TagId).WithTag(tag.Tag));
        Tags = result.ToList();
        return this;
    }

    public ContentResult WithLikesCount(int likesCount)
    {
        LikesCount = likesCount;
        return this;
    }
    
    public Guid Id { get; }
    public string Title { get; }
    public string Slug { get; }
    public Guid ContentTypeId { get; }
    public string? ContentTypeTitle { get; private set; }
    public string? ContentTypeSlug { get; private set; }
    public Guid LangId { get; }
    public string? LangTitle { get; private set; }
    public string? LangCode { get; private set; }
    public string Body { get; }
    public ContentBodyType BodyType { get; }
    public string AuthorUserId { get; }
    public string? AuthorUserName { get; private set; }
    public string? AuthorUserDisplayName { get; private set; }
    public string Summary { get; }
    public ContentStatus Status { get; }
    public DateTime? LastStatusChangedDate { get; }
    public DateTime? PublishDate { get; }
    public string? AltTitle { get; }
    public int OrderNum { get; }
    public string? IconName { get; }
    public string? ViewPath { get; }
    public DateTime CreatedDate { get; } 
    public DateTime? LastUpdated { get; }
    public string? CreatedByUserId { get; }
    public string? LastUpdatedByUserId { get; }
    public string? CreatedByIp { get; }
    public string? LastUpdatedByIp { get; }
    
    public IReadOnlyCollection<ContentCategoryItemResult> Categories { get; private set; }
    public IReadOnlyCollection<MetaResult> Meta { get; private set; }
    public IReadOnlyCollection<ContentFileResult> Files { get; private set; }
    public IReadOnlyCollection<ContentTagResult> Tags { get; private set; }
    public int LikesCount { get; private set; }
    
}