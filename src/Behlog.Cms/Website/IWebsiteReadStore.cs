using Behlog.Core;
using Behlog.Cms.Domain;

namespace Behlog.Cms.Store;


public interface IWebsiteReadStore : IBehlogReadStore<Website, Guid>
{

    Task<Website?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);


    Task<Website?> GetDefaultAsync(CancellationToken cancellationToken = default);
}