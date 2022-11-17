using Behlog.Cms.Exceptions;

namespace Behlog.Cms.Domain;

public partial class Website
{

    private async Task CheckNameExistAsync(IWebsiteService service)
    {
        if (await service.ExistByNameAsync(Name, Id))
            throw new WebsiteTitleExistException(Name);
    }
}