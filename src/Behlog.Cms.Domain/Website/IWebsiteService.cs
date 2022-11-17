namespace Behlog.Cms;


public interface IWebsiteService
{
    
    Task<bool> ExistByNameAsync(Guid? websiteId, string name);
    
}