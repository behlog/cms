using Behlog.Cms.Components.Exceptions;
using Behlog.Cms.Contracts;

namespace Behlog.Cms.Domain;

public partial class Component
{

    private static async Task GuardAgainstDuplicateName(
        Guid componentId, Guid websiteId, string name, IComponentService service)
    {
        if (await service.ComponentNameExistInWebsiteAsync(websiteId, componentId, name))
            throw new ComponentNameAlreadyExistedException(websiteId, name);
    }
}