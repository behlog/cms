using Behlog.Cms.Domain;
using Behlog.Core;
using Behlog.Core.Models;

namespace Behlog.Cms.Commands;


public class UpdateContentCommand : IBehlogCommand<CommandResult>
{
    
    public UpdateContentCommand(
        Guid id, string title, string slug, string body, 
        ContentBodyTypeEnum bodyType, ContentStatusEnum status, 
        Guid contentTypeId, string authorUserId, string summary, 
        string altTitle, int orderNum, IEnumerable<Guid> categories, 
        IEnumerable<MetaCommand> meta, IEnumerable<ContentFileCommand> files)
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
        Categories = categories?.ToList();
        Meta = meta?.ToList();
        Files = files?.ToList();
    }
    
    public Guid Id { get; }
    public string Title { get; }
    public string Slug { get; }
    public string Body { get; }
    public ContentBodyTypeEnum BodyType { get; }
    public ContentStatusEnum Status { get; }
    public Guid ContentTypeId { get; }
    public string AuthorUserId { get; }
    public string Summary { get; }
    public string AltTitle { get; }
    public int OrderNum { get; }
    public string Password { get; set; }
    public string IconName { get; set; }
    public IReadOnlyCollection<Guid>? Categories { get; }
    public IReadOnlyCollection<MetaCommand>? Meta { get; }
    public IReadOnlyCollection<ContentFileCommand>? Files { get; }
}