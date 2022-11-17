namespace Behlog.Cms;


public interface IWebsiteService
{
    
    Task<bool> ExistByNameAsync(string name, Guid websiteId);
    
}