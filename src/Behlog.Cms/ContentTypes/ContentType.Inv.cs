using Behlog.Cms.Contracts;
using Behlog.Cms.Exceptions;

namespace Behlog.Cms.Domain;

public partial class ContentType
{


    public static async Task CheckForDuplicatedSystemName(
        Guid id, Guid langId, string systemName, IContentTypeService service)
    {
        if (await service.ExistBySystemNameAsync(id, langId, systemName))
            throw new ContentTypeSystemNameExistedException(systemName);
    }
}