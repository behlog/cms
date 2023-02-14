namespace Behlog.Cms.Store;

public interface IWebsiteTagReadStore
{

    Task<bool> AnyAsync(
        Guid websiteId, Guid tagId, Guid contentId, CancellationToken cancellationToken = default);
    
    Task<IReadOnlyCollection<WebsiteTag>> FindAllAsync(
        Guid websiteId, CancellationToken cancellationToken = default);
    
    Task<WebsiteTag> FindAsync(long id);

    Task<IReadOnlyCollection<WebsiteTag>> FindAllAsync(
        Guid websiteId, Guid langId, CancellationToken cancellationToken = default);


    Task<IReadOnlyCollection<WebsiteTag>> FindByContentTypeAsync(
        Guid websiteId, Guid contentTypeId, CancellationToken cancellationToken = default);
}