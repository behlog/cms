using Behlog.Core;
using Behlog.Cms.Contracts;

namespace Behlog.Cms.Services;


public class WebsiteService : IWebsiteService
{
    private readonly IBehlogMediator _mediator;

    public WebsiteService(IBehlogMediator mediator)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }

    public async Task<bool> ExistByNameAsync(Guid? websiteId, string name)
    {
        return false;
    }
}