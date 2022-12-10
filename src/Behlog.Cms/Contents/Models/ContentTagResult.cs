using Behlog.Cms.Domain;

namespace Behlog.Cms.Models;

public class ContentTagResult
{
    public ContentTagResult(Guid contentId, Guid tagId)
    {
        ContentId = contentId;
        TagId = tagId;
    }


    public ContentTagResult WithTag(Tag tag)
    {
        if (tag is null) return this;

        Title = tag.Title;
        Slug = tag.Slug;
        return this;
    }
    
    public Guid ContentId { get; }
    public Guid TagId { get; }
    
    public string? Title { get; private set; }
    public string? Slug { get; private set; }
}