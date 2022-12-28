using Behlog.Core;

namespace Behlog.Cms.Domain;


public interface IComponentReadStore : IBehlogReadStore<Component, Guid>
{

    Task<Component?> GetByIdAsync(
        Guid id, CancellationToken cancellationToken = default);


    Task<Component?> GetByNameAsync(
        Guid websiteId, Guid langId, string name, CancellationToken cancellationToken = default);
    

    Task<IReadOnlyCollection<Component>> GetByComponentTypeAsync(
        string componentType, CancellationToken cancellationToken = default);


    Task<IReadOnlyCollection<Component>> GetByWebsiteIdAsync(
        Guid websiteId, CancellationToken cancellationToken = default);


    Task<bool> ExistByNameAsync(
        Guid websiteId, Guid langId, Guid componentId, string name, CancellationToken cancellationToken = default);

}