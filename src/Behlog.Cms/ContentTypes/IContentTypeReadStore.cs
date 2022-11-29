using Behlog.Core;

namespace Behlog.Cms.Store;

public interface IContentTypeReadStore : IBehlogReadStore<ContentType, Guid>
{


    Task<IReadOnlyCollection<ContentType>> GetByLangIdAsync(Guid langId);
}