using Behlog.Core;
using Behlog.Cms.Contracts;

namespace Behlog.Cms.Services;


public class ContentService : IContentService
{
    private readonly IBehlogMediator _mediator;
    
    public ContentService(IBehlogMediator mediator)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }
    
    public async Task<bool> SlugExistedInWebsiteAsync(Guid websiteId, Guid? contentId, string slug)
    {
        throw new NotImplementedException();
    }
}