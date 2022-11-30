using Behlog.Core;
using Behlog.Cms.Store;
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


    public async Task<ContentType> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _set.Include(_ => _.Language)
            .FirstOrDefaultAsync(_ => _.Id == id, cancellationToken).ConfigureAwait(false);
    }

    public async Task<IReadOnlyCollection<ContentType>> GetByLangIdAsync(
        Guid langId, CancellationToken cancellationToken = default)
    {
        return await _contentTypes.Where(_ => _.LangId == langId)
                                    .ToListAsync(cancellationToken).ConfigureAwait(false);
    }
}