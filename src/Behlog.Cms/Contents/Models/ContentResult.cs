using Behlog.Cms.Domain;

namespace Behlog.Cms.Models;


public class ContentResult
{

    public ContentResult() { }

    public ContentResult(
        Guid id, string title, string slug, Guid contentTypeId, Guid langId, string body,
        ContentBodyTypeEnum bodyType, string authorUserId, string summary, ContentStatusEnum status, string altTitle,
        int orderNum, DateTime? lastStatusChangedDate, DateTime? publishDate, string? coverPhoto, string? iconName, 
        string? viewPath, DateTime createdDate, DateTime? lastUpdated, string? createdByUserId, string? lastUpdatedByUserId,
        string? createdByIp, string? lastUpdatedByIp)
    {
        Id = id;
        Title = title;
        Slug = slug;
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
        CoverPhoto = coverPhoto;
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
    
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Slug { get; set; }
    public Guid ContentTypeId { get; set; }
    public string? ContentTypeTitle { get; set; }
    public string? ContentTypeSlug { get; set; }
    public Guid LangId { get; set; }
    public string? LangTitle { get; set; }
    public string? LangCode { get; set; }
    public string Body { get; set; }
    public ContentBodyTypeEnum BodyType { get; set; }
    public string AuthorUserId { get; set; }
    public string? AuthorUserName { get; set; }
    public string? AuthorUserDisplayName { get; set; }
    public string Summary { get; set; }
    public ContentStatusEnum Status { get; set; }
    public DateTime? LastStatusChangedDate { get; set; }
    public DateTime? PublishDate { get; set; }
    public string? AltTitle { get; set; }
    public int OrderNum { get; set; }
    public string? IconName { get; set; }
    public string? ViewPath { get; set; }
    public DateTime CreatedDate { get; set; } 
    public DateTime? LastUpdated { get; set; }
    public string? CreatedByUserId { get; set; }
    public string? LastUpdatedByUserId { get; set; }
    public string? CreatedByIp { get; set; }
    public string? LastUpdatedByIp { get; set; }
    public string? CoverPhoto { get; set; }
    public IReadOnlyCollection<ContentCategoryItemResult> Categories { get; set; }
    public IReadOnlyCollection<MetaResult> Meta { get; set; }
    public IReadOnlyCollection<ContentFileResult> Files { get; set; }
    public IReadOnlyCollection<ContentTagResult> Tags { get; set; }
    public int LikesCount { get; set; }
    
}