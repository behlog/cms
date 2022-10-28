using Behlog.Core;

namespace Behlog.Cms.Commands;

public class UpdateContentCategoryCommand : IBehlogCommand
{
    public UpdateContentCategoryCommand(
        Guid id, string title, string altTitle, string slug, 
        Guid? parentId, string description, Guid? contentTypeId)
    {
        Id = id;
        Title = title;
        AltTitle = altTitle;
        Slug = slug;
        ParentId = parentId;
        Description = description;
        ContentTypeId = contentTypeId;
    }
    
    public Guid Id { get; }
    public string Title { get; }
    public string AltTitle { get; }
    public string Slug { get; }
    public Guid? ParentId { get; }
    public string Description { get; }
    public Guid? ContentTypeId { get; }
}