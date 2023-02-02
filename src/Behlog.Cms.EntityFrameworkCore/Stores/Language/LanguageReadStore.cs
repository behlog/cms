using System.Globalization;
using Behlog.Cms.Store;
using Behlog.Cms.Domain;
using Microsoft.EntityFrameworkCore;
using Behlog.Core;

namespace Behlog.Cms.EntityFrameworkCore.Stores;

public class LanguageReadStore : BehlogEntityFrameworkCoreReadStore<Language, Guid>,
    ILanguageReadStore
{
    private readonly IQueryable<Language> _languages;

    public LanguageReadStore(IBehlogEntityFrameworkDbContext db) : base(db)
    {
        _languages = _set.Where(_=> _.Status != EntityStatus.Deleted);
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

    public async Task<IReadOnlyCollection<Language>> GetListAsync(
        EntityStatus? status = null, CancellationToken cancellationToken = default) {

        var query = _set.AsQueryable();
        if(status is not null) {
            query = query.Where(_ => _.Status == status.Value);
        }

        return await query.ToListAsync(cancellationToken).ConfigureAwait(false);
    }
}