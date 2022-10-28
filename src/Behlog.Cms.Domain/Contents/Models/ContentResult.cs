using Behlog.Core;
using Behlog.Cms.Domain;

namespace Behlog.Cms.Models;

public class ContentResult : BehlogResult
{

    public ContentResult(
        Guid id,
        string title,
        string slug,
        Guid contentTypeId,
        string contentTypeTitle,
        string contentTypeSlug,
        string body,
        ContentBodyType bodyType,
        string authorUserId,
        string summary,
        ContentStatus status,
        string altTitle,
        int orderNum,
        IReadOnlyCollection<Guid> categories) : base()
    {
        Id = id;
        Title = title;
        Slug = slug;;
        ContentTypeId = contentTypeId;
        ContentTypeTitle = contentTypeTitle;
        ContentTypeSlug = contentTypeSlug;
        Body = body;
        BodyType = bodyType;
        AuthorUserId = authorUserId;
        Summary = summary;
        Status = status;
        AltTitle = altTitle;
        OrderNum = orderNum;
        Categories = categories;
    }
    
    public Guid Id { get; }
    public string Title { get; }
    public string Slug { get; }
    public Guid ContentTypeId { get; }
    public string ContentTypeTitle { get; }
    public string ContentTypeSlug { get; }
    public string Body { get; }
    public ContentBodyType BodyType { get; }
    public string AuthorUserId { get; }
    public string Summary { get; }
    public ContentStatus Status { get; }
    public string AltTitle { get; }
    public int OrderNum { get; }
    public IReadOnlyCollection<Guid> Categories { get; protected set; } = new List<Guid>();
    
}