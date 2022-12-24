using Behlog.Cms.Contracts;
using Behlog.Cms.Exceptions;

namespace Behlog.Cms.Domain;


public partial class Component
{

    private static async Task GuardAgainstDuplicateName(
        Guid componentId, Guid websiteId, Guid langId, string name, IComponentService service)
    {
        if (await service.ComponentNameExistInWebsiteAsync(websiteId, langId, componentId, name))
            throw new ComponentNameAlreadyExistedException(websiteId, name);
    }
}