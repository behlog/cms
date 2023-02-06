namespace Behlog.Cms.Commands;


public class UpdateContentCommand : IBehlogCommand<CommandResult>
{

    public UpdateContentCommand(Guid id)
    {
        Id = id;
    }
    
    public UpdateContentCommand(
        Guid id, string title, string slug, string body, 
        ContentBodyTypeEnum bodyType,
        Guid contentTypeId, string? summary, 
        string altTitle, int orderNum, IEnumerable<Guid> categories, 
        IEnumerable<MetaCommand> meta, IEnumerable<ContentFileCommand> files)
    {
        Id = id;
        Title = title;
        Slug = slug;
        Body = body;
        BodyType = bodyType;
        ContentTypeId = contentTypeId;
        Summary = summary;
        AltTitle = altTitle;
        OrderNum = orderNum;
        Categories = categories?.ToList();
        Meta = meta?.ToList();
        Files = files?.ToList();
    }
    
    public Guid Id { get; }
    public string Title { get; set; }
    public string Slug { get; set; }
    public string Body { get; set; }
    public ContentBodyTypeEnum BodyType { get; set; }
    public bool IsDraft { get; set; }
    public Guid ContentTypeId { get; set; }
    public string? Summary { get; set; }
    public string? AltTitle { get; set; }
    public int OrderNum { get; set; }
    public string? Password { get; set; }
    public string? IconName { get; set; }
    public string? ViewPath { get; set; }
    public DateTime? PublishDate { get; set; }
    public IReadOnlyCollection<Guid>? Categories { get; }
    public IReadOnlyCollection<MetaCommand>? Meta { get; }
    public IReadOnlyCollection<ContentFileCommand>? Files { get; }

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
    
}