namespace Behlog.Cms.Contracts;


public interface IWebsiteService
{
    
    Task<bool> ExistByNameAsync(Guid? websiteId, string name);
    
}