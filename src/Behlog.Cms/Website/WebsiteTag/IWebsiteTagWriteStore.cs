namespace Behlog.Cms.Store;

public interface IWebsiteTagWriteStore
{
    Task AddAsync(WebsiteTag websiteTag, CancellationToken cancellationToken = default);

    Task UpdateAsync(WebsiteTag websiteTag, CancellationToken cancellationToken = default);

    Task DeleteAsync(WebsiteTag websiteTag, CancellationToken cancellationToken = default);
}