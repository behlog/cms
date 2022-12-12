namespace Behlog.Cms.Contracts;

public interface IContentTypeService
{


    Task<bool> ExistBySystemNameAsync(Guid langId, string systemName);
}