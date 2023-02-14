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
        LangId = tag.LangId;

        return this;
    }
    
    public Guid ContentId { get; }
    public Guid TagId { get; }

    public Guid LangId { get; private set; }
    
    public string? Title { get; private set; }
    public string? Slug { get; private set; }
}