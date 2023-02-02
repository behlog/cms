using Behlog.Core;
using Behlog.Cms.Store;
using Behlog.Cms.Domain;
using Behlog.Extensions;
using Microsoft.EntityFrameworkCore;
using Behlog.Cms.Query;
using Behlog.Core.Models;
using Behlog.Cms.EntityFrameworkCore.Extensions;

namespace Behlog.Cms.EntityFrameworkCore.Stores;


public class ContentTypeReadStore : BehlogEntityFrameworkCoreReadStore<ContentType, Guid>, IContentTypeReadStore
{
    private readonly IQueryable<ContentType> _contentTypes;

    public ContentTypeReadStore(IBehlogEntityFrameworkDbContext db) 
        : base(db)
    {
        _contentTypes = db.Set<ContentType>().Where(_=> _.Status != EntityStatus.Deleted);
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

        systemName = systemName.ToUpperInvariant();
        
        return await _set.FirstOrDefaultAsync(_ => _.LangId == langId &&
                                                    _.SystemName.ToUpper() == systemName,
            cancellationToken).ConfigureAwait(false);
    }

    
    /// <inheritdoc />
    public async Task<IReadOnlyCollection<ContentType>> GetByLangIdAsync(
        Guid langId, EntityStatus? status = null, CancellationToken cancellationToken = default)
    {
        var query = _contentTypes.Where(_ => _.LangId == langId);
        if(status is not null) {
            query = query.Where(_ => _.Status == status.Value);
        }

        return await query.ToListAsync(cancellationToken).ConfigureAwait(false);
    }

    /// <inheritdoc /> 
    public async Task<bool> ExistBySystemNameAsync(
        Guid id, Guid langId, string systemName, CancellationToken cancellationToken = default)
    {
        return await _set.AnyAsync(_ => _.Id != id && _.LangId == langId &&
                                        _.SystemName.ToUpper() == systemName.ToUpper(), 
            cancellationToken: cancellationToken).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public async Task<ContentType?> QueryAsync(
        QueryActiveContentType model, CancellationToken cancellationToken = default) 
    {
        model.ThrowExceptionIfArgumentIsNull(nameof(model));
        if (model.SystemName.IsNullOrEmpty())
            throw new ArgumentNullException(nameof(model.SystemName));

        var query = _set.Where(x => x.Status == EntityStatus.Enabled)
            .Where(x => x.SystemName.ToUpper() == model.SystemName.ToUpper());

        if(model.LangId != default) {
            query = query.Where(x => x.LangId == model.LangId);
        }
        else if(model.LangCode.IsNotNullOrEmpty()) {
            query = query.Where(x => x.Language.Code.ToUpper() == model.LangCode.ToUpper());
        }

        return await query.Include(_ => _.Language).FirstOrDefaultAsync();
    }

    /// <inheritdoc />
    public async Task<QueryResult<ContentType>> QueryAsync(
        QueryAdminContentType model, CancellationToken cancellationToken = default) {
        model.ThrowExceptionIfArgumentIsNull(nameof(model));

        var query = _contentTypes.AsQueryable();

        if(model.LangId != null && model.LangId != default) {
            query = query.Where(x => x.LangId == model.LangId.Value);
        }
        else if(model.LangCode.IsNotNullOrEmpty()) {
            query = query.Where(x => x.Language.Code.ToUpper() == model.LangCode.ToUpper());
        }

        if(model.SystemName.IsNotNullOrEmpty()) {
            query = query.Where(_ => _.SystemName.ToUpper() == model.SystemName.ToUpper());
        }

        if(model.Options.Search.IsNotNullOrEmpty()) {
            var search = model.Options.Search.ToUpper().CorrectYeKe().Trim();
            query = query.Where(x => x.Title.ToUpper().Contains(search) ||
                                    x.Slug.ToUpper().Contains(search) ||
                                    x.SystemName.ToUpper().Contains(search) ||
                                    x.Description.ToUpper().Contains(search));
        }

        return QueryResult<ContentType>.Create()
            .WithPageNumber(model.Options.PageNumber)
            .WithPageSize(model.Options.PageSize)
            .WithTotalCount(await query.LongCountAsync(cancellationToken))
            .WithResults(
                await query.Skip(model.Options.StartIndex)
                            .Take(model.Options.PageSize)
                            .SortBy(model.Options.OrderBy, model.Options.OrderDesc)
                            .ToListAsync(cancellationToken));
    }
}