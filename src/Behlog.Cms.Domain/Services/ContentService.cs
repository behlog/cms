using Behlog.Cms.Contracts;
using Behlog.Core;

namespace Behlog.Cms.Services;

public class ContentService : IContentService
{
    private readonly IBehlogManager _manager;


    public ContentService(IBehlogManager manager)
    {
        _manager = manager ?? throw new ArgumentNullException(nameof(manager));
    }
    
    public async Task<bool> SlugExistedInWebsiteAsync(Guid websiteId, Guid? contentId, string slug)
    {
        throw new NotImplementedException();
    }
}