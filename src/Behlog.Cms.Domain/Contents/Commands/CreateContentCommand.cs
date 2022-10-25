using Behlog.Cms.Domain;
using Behlog.Core;

namespace Behlog.Cms.Commands;

public class CreateContentCommand : IBehlogCommand
{
    
    public CreateContentCommand(
        string title, string slug, Guid contentTypeId, 
        string body, ContentBodyType bodyType, string authorUserId, 
        string summary, string altTitle, int orderNum, IEnumerable<Guid> categories)
    {
        Title = title;
        Slug = slug;
        ContentTypeId = contentTypeId;
        Body = body;
        BodyType = bodyType;
        AuthorUserId = authorUserId;
        Summary = summary;
        AltTitle = altTitle;
        OrderNum = orderNum;
        Categories = categories?.ToList();
    }

    public string Title { get; }
    public string Slug { get; }
    public Guid ContentTypeId { get; }
    public string Body { get; }
    public ContentBodyType BodyType { get; }
    public string AuthorUserId { get; }
    public string Summary { get; }
    public string AltTitle { get; }
    public int OrderNum { get; set; }
    
    public IReadOnlyCollection<Guid>? Categories { get; }
}