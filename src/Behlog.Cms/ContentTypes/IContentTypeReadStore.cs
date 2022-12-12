using Behlog.Core;

namespace Behlog.Cms.Store;

public interface IContentTypeReadStore : IBehlogReadStore<ContentType, Guid>
{

    Task<ContentType?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);


    Task<ContentType?> GetBySystemNameAsync(
        Guid langId, string systemName, CancellationToken cancellationToken = default);
    

    Task<IReadOnlyCollection<ContentType>> GetByLangIdAsync(
        Guid langId, CancellationToken cancellationToken = default);

    
    Task<bool> ExistBySystemNameAsync(
        Guid langId, string systemName, CancellationToken cancellationToken = default);
}