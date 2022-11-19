using Behlog.Cms.Contracts;
using Behlog.Cms.Exceptions;

namespace Behlog.Cms.Domain;

public partial class Website
{

    private static async Task CheckNameExistAsync(
        Guid? id, string name, IWebsiteService service)
    {
        if (await service.ExistByNameAsync(id, name))
            throw new WebsiteTitleExistException(name);
    }
}