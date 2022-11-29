using Behlog.Cms.Domain;
using Behlog.Core;

namespace Behlog.Cms.Commands;

public class UpdateContentCommand : IBehlogCommand
{
    
    public UpdateContentCommand(
        Guid id, string title, string slug, string body, 
        ContentBodyType bodyType, ContentStatus status, 
        Guid contentTypeId, string authorUserId, string summary, 
        string altTitle, int orderNum, IEnumerable<Guid> categories, 
        IEnumerable<MetaCommand> meta)
    {
        Id = id;
        Title = title;
        Slug = slug;
        Body = body;
        BodyType = bodyType;
        Status = status;
        ContentTypeId = contentTypeId;
        AuthorUserId = authorUserId;
        Summary = summary;
        AltTitle = altTitle;
        OrderNum = orderNum;
        Categories = categories?.ToList()!;
        Meta = meta?.ToList()!;
    }
    
    public Guid Id { get; }
    public string Title { get; }
    public string Slug { get; }
    public string Body { get; }
    public ContentBodyType BodyType { get; }
    public ContentStatus Status { get; }
    public Guid ContentTypeId { get; }
    public string AuthorUserId { get; }
    public string Summary { get; }
    public string AltTitle { get; }
    public int OrderNum { get; }
    public string Password { get; set; }
    public string IconName { get; set; }
    public IReadOnlyCollection<Guid> Categories { get; }
    public IReadOnlyCollection<MetaCommand> Meta { get; }
}