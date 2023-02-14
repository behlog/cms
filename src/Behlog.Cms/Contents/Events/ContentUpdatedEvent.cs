namespace Behlog.Cms.Events;

public class ContentUpdatedEvent : BehlogDomainEvent
{

    public ContentUpdatedEvent(
        Guid id,
        string title,
        string slug,
        Guid websiteId,
        Guid contentTypeId,
        Guid langId,
        string body,
        ContentBodyType bodyType,
        string authorUserId,
        string summary,
        ContentStatusEnum status,
        string altTitle,
        int orderNum,
        IReadOnlyCollection<Guid> categories,
        IReadOnlyCollection<ContentMeta> meta,
        IReadOnlyCollection<ContentFile> files,
        IReadOnlyCollection<ContentTagEventData> tags)
    {
        Id = id;
        Title = title;
        Slug = slug;
        WebsiteId = websiteId;
        ContentTypeId = contentTypeId;
        LangId = langId;
        Body = body;
        BodyType = bodyType;
        AuthorUserId = authorUserId;
        Summary = summary;
        Status = status;
        AltTitle = altTitle;
        OrderNum = orderNum;
        Categories = categories;
        Files = files;
        Meta = meta;
        Tags = tags;
    }

    public Guid Id { get; }
    public string Title { get; }
    public string Slug { get; }
    public Guid ContentTypeId { get; }
	public Guid WebsiteId { get; }
	public Guid LangId { get; }
	public string Body { get; }
    public ContentBodyType BodyType { get; }
    public string AuthorUserId { get; }
    public string Summary { get; }
    public ContentStatusEnum Status { get; }
    public string AltTitle { get; }
    public int OrderNum { get; }
    public IReadOnlyCollection<Guid> Categories { get; }
    public IReadOnlyCollection<ContentFile> Files { get; }
    public IReadOnlyCollection<ContentMeta> Meta { get; }
    public IReadOnlyCollection<ContentTagEventData> Tags { get; }
}