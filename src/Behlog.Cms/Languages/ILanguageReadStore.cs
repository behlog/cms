using Behlog.Core;
using Behlog.Cms.Domain;

namespace Behlog.Cms.Store;


public interface ILanguageReadStore : IBehlogReadStore<Language, Guid>
{


    Task<Language?> GetByCodeAsync(string langCode, CancellationToken cancellationToken = default);
}