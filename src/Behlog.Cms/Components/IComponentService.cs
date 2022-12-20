using Behlog.Cms.Domain;

namespace Behlog.Cms.Contracts;


public interface IComponentService
{
    /// <summary>
    /// Checks if a <see cref="Component"/> exists in a <see cref="Website"/> by it's name.
    /// </summary>
    /// <param name="websiteId"></param>
    /// <param name="componentId"></param>
    /// <param name="componentName"></param>
    Task<bool> ComponentNameExistInWebsiteAsync(
        Guid websiteId, Guid componentId, string componentName);
    
}