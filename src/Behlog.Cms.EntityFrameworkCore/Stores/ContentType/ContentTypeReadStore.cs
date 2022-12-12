using System.Globalization;
using Behlog.Core;
using Behlog.Cms.Store;
using Behlog.Extensions;
using Microsoft.EntityFrameworkCore;

namespace Behlog.Cms.EntityFrameworkCore.Stores;


public class ContentTypeReadStore : BehlogReadStore<ContentType, Guid>, IContentTypeReadStore
{
    private readonly DbSet<ContentType> _contentTypes;

    public ContentTypeReadStore(IBehlogEntityFrameworkDbContext db) 
        : base(db)
    {
        _contentTypes = db.Set<ContentType>();
    }


    /// <inheritdoc />
    public async Task<ContentType?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _set.Include(_ => _.Language)
            .FirstOrDefaultAsync(_ => _.Id == id, cancellationToken).ConfigureAwait(false);
    }

    
    /// <inheritdoc />
    public async Task<ContentType?> GetBySystemNameAsync(
        Guid langId, string systemName, CancellationToken cancellationToken = default)
    {
        if (systemName.IsNullOrEmpty())
            throw new ArgumentNullException(nameof(systemName));

        return await _set.FirstOrDefaultAsync(_ => _.LangId == langId &&
            _.SystemName.ToUpper(CultureInfo.InvariantCulture) == systemName.ToUpper(CultureInfo.InvariantCulture),
            cancellationToken).ConfigureAwait(false);
    }

    
    /// <inheritdoc />
    public async Task<IReadOnlyCollection<ContentType>> GetByLangIdAsync(
        Guid langId, CancellationToken cancellationToken = default)
    {
        return await _contentTypes.Where(_ => _.LangId == langId)
                                    .ToListAsync(cancellationToken).ConfigureAwait(false);
    }

    /// <inheritdoc /> 
    public async Task<bool> ExistBySystemNameAsync(
        Guid langId, string systemName, CancellationToken cancellationToken = default)
    {
        return await _set.AnyAsync(_ => _.LangId == langId &&
                                        _.SystemName.ToUpper() == systemName.ToUpper()).ConfigureAwait(false);
    }
}