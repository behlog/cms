using Behlog.Core;

namespace Behlog.Cms.Commands;

public class UpdateContentCategoryCommand : IBehlogCommand
{
    public UpdateContentCategoryCommand(
        Guid id, string title, string altTitle, string slug, Guid langId,
        Guid? parentId, string description, bool enabled = true)
    {
        Id = id;
        Title = title;
        AltTitle = altTitle;
        Slug = slug;
        LangId = langId;
        ParentId = parentId;
        Description = description;
        Enabled = enabled;
    }
    
    public Guid Id { get; }
    public string Title { get; }
    public string AltTitle { get; }
    public string Slug { get; }
    public Guid LangId { get; }
    public Guid? ParentId { get; }
    public string Description { get; }
    public bool Enabled { get; }
}