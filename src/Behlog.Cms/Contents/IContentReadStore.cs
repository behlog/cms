using Behlog.Core;

namespace Behlog.Cms.Domain;

public interface IContentReadStore : IBehlogReadStore<Content, Guid>
{

    Task<Content?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    
    
}