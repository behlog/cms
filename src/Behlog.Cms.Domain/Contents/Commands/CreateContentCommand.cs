using Behlog.Cms.Domain;
using Behlog.Cms.Models;
using Behlog.Core;
using Behlog.Core.Models;

namespace Behlog.Cms.Commands;

public class CreateContentCommand : IBehlogCommand<CommandResult<ContentResult>>
{
    
    public CreateContentCommand(
        Guid websiteId,
        string title, string slug, Guid contentTypeId, 
        string body, ContentBodyType bodyType, 
        string summary, string altTitle, int orderNum, 
        IEnumerable<Guid> categories)
    {
        WebsiteId = websiteId;
        Title = title;
        Slug = slug;
        ContentTypeId = contentTypeId;
        Body = body;
        BodyType = bodyType;
        Summary = summary;
        AltTitle = altTitle;
        OrderNum = orderNum;
        Categories = categories?.ToList();
    }
    
    public Guid WebsiteId { get; }
    public string Title { get; }
    public string Slug { get; }
    public Guid ContentTypeId { get; }
    public string Body { get; }
    public ContentBodyType BodyType { get; }
    public string Summary { get; }
    public string AltTitle { get; }
    public int OrderNum { get; }
    
    public string? Password { get; set; }
    public string? IconName { get; set; }
    public string? ViewPath { get; set; }
    
    
    public IReadOnlyCollection<Guid>? Categories { get; }
}