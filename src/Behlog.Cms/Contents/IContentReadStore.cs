using Behlog.Core;

namespace Behlog.Cms.Domain;

public interface IContentReadStore : IBehlogReadStore<Content, Guid>
{

    Task<Content?> GetByIdAsync(
        Guid id, CancellationToken cancellationToken = default);
    
    Task<int> CountLikesAsync(
        Guid id, CancellationToken cancellationToken = default);
    
    Task<Content?> GetBySlugAsync(
        Guid websiteId, string slug, CancellationToken cancellationToken = default);
}