using System.Globalization;
using Behlog.Cms.Store;
using Behlog.Cms.Domain;
using Microsoft.EntityFrameworkCore;

namespace Behlog.Cms.EntityFrameworkCore.Stores;

public class LanguageReadStore : BehlogEntityFrameworkCoreReadStore<Language, Guid>,
    ILanguageReadStore
{

    public LanguageReadStore(IBehlogEntityFrameworkDbContext db) : base(db)
    {
    }

    public async Task<Language?> GetByCodeAsync(string langCode, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(langCode))
            throw new ArgumentNullException(nameof(langCode));

        return await _set.FirstOrDefaultAsync(_ =>
            _.Code.ToUpper(CultureInfo.InvariantCulture) == langCode.ToUpper(CultureInfo.InvariantCulture), 
            cancellationToken
            ).ConfigureAwait(false);
    }
}