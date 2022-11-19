using Behlog.Cms.Contracts;
using Behlog.Core;

namespace Behlog.Cms.Services;

public class WebsiteService : IWebsiteService
{
    private readonly IBehlogManager _manager;

    public WebsiteService(IBehlogManager manager)
    {
        _manager = manager ?? throw new ArgumentNullException(nameof(manager));
    }

    public async Task<bool> ExistByNameAsync(Guid? websiteId, string name)
    {
        throw new NotImplementedException();
    }
}