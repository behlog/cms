namespace Behlog.Cms.Store;

public interface IWebsiteTagReadStore
{

    Task<bool> AnyAsync(
        Guid websiteId, Guid tagId, CancellationToken cancellationToken = default);
    
    Task<IReadOnlyCollection<WebsiteTag>> FindAllAsync(
        Guid websiteId, CancellationToken cancellationToken = default);
    
    Task<WebsiteTag> FindAsync(Guid websiteId, Guid tagId);

    Task<IReadOnlyCollection<WebsiteTag>> FindAllAsync(
        Guid websiteId, Guid langId, CancellationToken cancellationToken = default);
}