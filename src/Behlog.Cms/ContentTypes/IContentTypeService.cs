namespace Behlog.Cms.Contracts;

public interface IContentTypeService
{


    Task<bool> ExistBySystemNameAsync(Guid id, Guid langId, string systemName);
}