using Behlog.Core;
using Behlog.Cms.Domain;
using Behlog.Core.Models;

namespace Behlog.Cms.Store;

public interface IContentTypeReadStore : IBehlogReadStore<ContentType, Guid>
{

    Task<ContentType?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);


    Task<ContentType?> GetBySystemNameAsync(
        Guid langId, string systemName, CancellationToken cancellationToken = default);
    

    Task<IReadOnlyCollection<ContentType>> GetByLangIdAsync(
        Guid langId, EntityStatus? status = null, CancellationToken cancellationToken = default);

    
    Task<bool> ExistBySystemNameAsync(
        Guid id, Guid langId, string systemName, CancellationToken cancellationToken = default);

    Task<ContentType?> QueryAsync(
        QueryActiveContentType model, CancellationToken cancellationToken = default);


    Task<QueryResult<ContentType>> QueryAsync(
        QueryAdminContentType model, CancellationToken cancellationToken = default);
}