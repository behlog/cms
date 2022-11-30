using Behlog.Core;

namespace Behlog.Cms.Store;

public interface IContentTypeReadStore : IBehlogReadStore<ContentType, Guid>
{

    Task<ContentType> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    

    Task<IReadOnlyCollection<ContentType>> GetByLangIdAsync(
        Guid langId, CancellationToken cancellationToken = default);
}