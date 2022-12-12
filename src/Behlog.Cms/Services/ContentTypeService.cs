using Behlog.Cms.Store;
using Behlog.Cms.Contracts;

namespace Behlog.Cms.Services;


public class ContentTypeService : IContentTypeService
{
    private readonly IContentTypeReadStore _readStore;

    public ContentTypeService(IContentTypeReadStore readStore)
    {
        _readStore = readStore ?? throw new ArgumentNullException(nameof(readStore));
    }

    public async Task<bool> ExistBySystemNameAsync(Guid id, Guid langId, string systemName)
    {
        return await _readStore.ExistBySystemNameAsync(id, langId, systemName).ConfigureAwait(false);
    }
}