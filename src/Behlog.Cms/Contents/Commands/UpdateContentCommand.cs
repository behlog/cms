namespace Behlog.Cms.Commands;


public class UpdateContentCommand : IBehlogCommand<CommandResult>
{

    public UpdateContentCommand(Guid id)
    {
        Id = id;
    }

    public UpdateContentCommand(Guid id, Guid langId, Guid contentTypeId)
    {
        Id = id;
        LangId = langId;
        ContentTypeId = contentTypeId;
    }

    public Guid Id { get; }
    public string Title { get; set; }
    public string Slug { get; set; }
    public string Body { get; set; }
    public ContentBodyTypeEnum BodyType { get; set; }
    public bool IsDraft { get; set; }
    public Guid ContentTypeId { get; set; }
    public Guid LangId { get; set; }
    public string? Summary { get; set; }
    public string? AltTitle { get; set; }
    public int OrderNum { get; set; }
    public string? Password { get; set; }
    public string? IconName { get; set; }
    public string? ViewPath { get; set; }
    public DateTime? PublishDate { get; set; }
    public IReadOnlyCollection<Guid>? Categories { get; set; }
    public IReadOnlyCollection<MetaCommand>? Meta { get; set; }
    public IReadOnlyCollection<ContentFileCommand>? Files { get; set; }
    public IReadOnlyCollection<Guid> Tags { get; set; }

    public UpdateContentCommand WithTitle(string title)
    {
        Title = title;
        return this;
    }

    public UpdateContentCommand WithAltTitle(string altTitle)
    {
        AltTitle = altTitle;
        return this;
    }

    public UpdateContentCommand WithSlug(string slug)
    {
        Slug = slug;
        return this;
    }

    public UpdateContentCommand WithSummary(string summary)
    {
        Summary = summary;
        return this;
    }

    public UpdateContentCommand WithBody(string body)
    {
        Body = body;
        return this;
    }

    public UpdateContentCommand WithBodyType(ContentBodyTypeEnum bodyType)
    {
        BodyType = bodyType;
        return this;
    }

    public UpdateContentCommand WithOrderNum(int orderNum)
    {
        OrderNum = orderNum;
        return this;
    }

    public UpdateContentCommand WillBeDraft()
    {
        IsDraft = true;
        return this;
    }

    public UpdateContentCommand WillBePublishedOn(DateTime publishDate)
    {
        PublishDate = publishDate;
        return this;
    }

    public UpdateContentCommand WithPassword(string password)
    {
        Password = password;
        return this;
    }

    public UpdateContentCommand WithIconName(string iconName)
    {
        IconName = iconName;
        return this;
    }

    public UpdateContentCommand WithViewPath(string viewPath)
    {
        ViewPath = viewPath;
        return this;
    }

    public UpdateContentCommand WithCategories(IReadOnlyCollection<Guid> categories)
    {
        Categories = categories?.ToList();
        return this;
    }

    public UpdateContentCommand WithFiles(IReadOnlyCollection<ContentFileCommand> files)
    {
        Files = files?.ToList();
        return this;
    }

    public UpdateContentCommand WithMeta(IReadOnlyCollection<MetaCommand> meta)
    {
        Meta = meta?.ToList();
        return this;
    }

    public UpdateContentCommand WithTags(IReadOnlyCollection<Guid> tags)
    {
        Tags = tags;
        return this;
    }
}