using Behlog.Core;

namespace Behlog.Cms.Exceptions;

public class ContentSlugAlreadyExistedException : BehlogException
{

    public ContentSlugAlreadyExistedException(Guid websiteId, string slug)
        : base($"The Website with Id: [{websiteId.ToString()}] already have a content with slug '{slug}'. The Content slug must be unique across Websites.")
    {
        WebsiteId = websiteId;
        ExistedSlug = slug;
    }
    
    public Guid WebsiteId { get; }
    
    public string ExistedSlug { get; }
}