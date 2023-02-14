namespace Behlog.Cms.Commands;


public class CreateContentCommand : IBehlogCommand<CommandResult<ContentResult>>
{

    public CreateContentCommand(Guid websiteId, Guid langId, Guid contentTypeId)
    {
        WebsiteId = websiteId;
        LangId = langId;
        ContentTypeId = contentTypeId;
    }
    
    public CreateContentCommand(
        Guid websiteId,
        string title, string slug, Guid contentTypeId, Guid langId, 
        string body, ContentBodyType bodyType, 
        string summary, string altTitle, int orderNum, 
        IEnumerable<Guid> categories, IEnumerable<MetaCommand> meta,
        IEnumerable<ContentFileCommand> files, IEnumerable<Guid>? tags)
    {
        WebsiteId = websiteId;
        Title = title;
        Slug = slug;
        ContentTypeId = contentTypeId;
        LangId = langId;
        Body = body;
        BodyType = bodyType;
        Summary = summary;
        AltTitle = altTitle;
        OrderNum = orderNum;
        Categories = categories?.ToList();
        Meta = meta?.ToList();
        Files = files?.ToList();
        Tags = tags?.ToList();
    }
    
    public Guid WebsiteId { get; }
    public string Title { get; set; }
    public string Slug { get; set; }
    public Guid ContentTypeId { get; }
    public string ContentTypeName { get; set; }
    public string Body { get; set; }
    public ContentBodyType BodyType { get; set; }
    public string Summary { get; set; }
    public string AltTitle { get; set; }
    public int OrderNum { get; set; }
    public Guid LangId { get; }
    public string? Password { get; set; }
    public string? IconName { get; set; }
    public string? ViewPath { get; set; }
    public DateTime? PublishDate { get; set; }
    public IFormFile? CoverPhotoFile { get; set; }

    public IReadOnlyCollection<Guid>? Categories { get; set; }
    
    public IReadOnlyCollection<MetaCommand>? Meta { get; set; }
    
    public IReadOnlyCollection<ContentFileCommand>? Files { get; set; }
    
    public IReadOnlyCollection<Guid>? Tags { get; set; } 

    public CreateContentCommand WithTitle(string title)
    {
        Title = title;
        return this;
    }

    public CreateContentCommand WithContentTypeName(string contentTypeName)
    {
        ContentTypeName = contentTypeName;
        return this;
    }

    public CreateContentCommand WithBody(string body)
    {
        Body = body;
        return this;
    }
    
    public CreateContentCommand WithPassword(string password)
    {
        Password = password;
        return this;
    }

    public CreateContentCommand WithIconName(string iconName)
    {
        IconName = iconName;
        return this;
    }

    public CreateContentCommand WillBePublishedOn(DateTime publishDate)
    {
        PublishDate = publishDate;
        return this;
    }

    public CreateContentCommand WithCategories(IReadOnlyCollection<Guid> categories)
    {
        Categories = categories;
        return this;
    }

    public CreateContentCommand WithMeta(IReadOnlyCollection<MetaCommand> meta)
    {
        Meta = meta;
        return this;
    }

    public CreateContentCommand WithFiles(IReadOnlyCollection<ContentFileCommand> files)
    {
        Files = files;
        return this;
    }

    public CreateContentCommand WithTags(IReadOnlyCollection<Guid> tags)
    {
        Tags = tags;
        return this;
    }

    public CreateContentCommand WithCoverPhotoFile(IFormFile coverPhotoFile)
    {
        CoverPhotoFile = coverPhotoFile;
        return this;
    }
}