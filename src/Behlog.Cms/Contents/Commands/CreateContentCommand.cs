using Behlog.Core;
using Behlog.Cms.Domain;
using Behlog.Cms.Models;
using Behlog.Core.Models;

namespace Behlog.Cms.Commands;


public class CreateContentCommand : IBehlogCommand<CommandResult<ContentResult>>
{
    
    public CreateContentCommand(
        Guid websiteId,
        string title, string slug, Guid contentTypeId, Guid langId, 
        string body, ContentBodyTypeEnum bodyType, 
        string summary, string altTitle, int orderNum, 
        IEnumerable<Guid> categories, IEnumerable<MetaCommand> meta,
        IEnumerable<ContentFileCommand> files)
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
    }
    
    public Guid WebsiteId { get; }
    public string Title { get; }
    public string Slug { get; }
    public Guid ContentTypeId { get; }
    public string Body { get; }
    public ContentBodyTypeEnum BodyType { get; }
    public string Summary { get; }
    public string AltTitle { get; }
    public int OrderNum { get; }
    public Guid LangId { get; }
    public string? Password { get; set; }
    public string? IconName { get; set; }
    public string? ViewPath { get; set; }

    public IReadOnlyCollection<Guid>? Categories { get; }
    
    public IReadOnlyCollection<MetaCommand>? Meta { get; }
    
    public IReadOnlyCollection<ContentFileCommand>? Files { get; }
}