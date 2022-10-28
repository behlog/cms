using Behlog.Cms.Models;
using Behlog.Core;

namespace Behlog.Cms.Commands;

public class CreateContentCategoryCommand : IBehlogCommand<ContentCategoryResult>
{
    public CreateContentCategoryCommand(
        string title, string altTitle, string slug, 
        Guid? parentId, string description, Guid? contentTypeId)
    {
        Title = title;
        AltTitle = altTitle;
        Slug = slug;
        ParentId = parentId;
        Description = description;
        ContentTypeId = contentTypeId;
    }
    
    public string Title { get; }
    public string AltTitle { get; }
    public string Slug { get; }
    public Guid? ParentId { get; }
    public string Description { get; }
    public Guid? ContentTypeId { get; }
}