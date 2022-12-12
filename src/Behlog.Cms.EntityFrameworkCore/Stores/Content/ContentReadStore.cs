using Behlog.Cms.Domain;
using Behlog.Cms.Query;
using Behlog.Extensions;
using Microsoft.EntityFrameworkCore;

namespace Behlog.Cms.EntityFrameworkCore.Stores;

public class ContentReadStore : BehlogReadStore<Content, Guid>, IContentReadStore
{
    public ContentReadStore(IBehlogEntityFrameworkDbContext db) 
        : base(db)
    {
    }

    /// <inheritdoc />
    public async Task<Content?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _set
            .Include(_ => _.Categories)
            .Include(_ => _.Blocks)
            .Include(_ => _.Components)
            .Include(_ => _.Files)
            .Include(_ => _.Language)
            .Include(_ => _.Meta)
            .Include(_ => _.ContentType)
            .FirstOrDefaultAsync(_ => _.Id == id, cancellationToken).ConfigureAwait(false);
    }
    
    /// <inheritdoc /> 
    public async Task<int> CountLikesAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _db.Set<ContentLike>().CountAsync(
            _ => _.ContentId == id, cancellationToken).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public async Task<Content?> GetBySlugAsync(
        Guid websiteId, string slug, CancellationToken cancellationToken = default)
    {
        return await _set
            .Include(_ => _.Categories)
            .Include(_ => _.Blocks)
            .Include(_ => _.Components)
            .Include(_ => _.Files)
            .Include(_ => _.Language)
            .Include(_ => _.Meta)
            .Include(_ => _.ContentType)
            .FirstOrDefaultAsync(_ => _.WebsiteId == websiteId &&
                                      _.Slug.ToUpper() == slug.ToUpper(), cancellationToken)
            .ConfigureAwait(false);
    }

    /// <inheritdoc />
    public async Task<IReadOnlyCollection<Content>> GetLatestByWebsiteIdAsync(
        Guid websiteId, int take = 10, CancellationToken cancellationToken = default)
    {
        return await _set
            .Include(_=> _.Categories)
            .Include(_=> _.Tags)
            .Include(_=> _.ContentType)
            .Include(_=> _.Language)
            .Where(_ => _.WebsiteId == websiteId)
                            .OrderByDescending(_=> _.CreatedDate)
                            .Take(take)
                            .ToListAsync(cancellationToken).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public async Task<IReadOnlyCollection<Content>> GetLatestByContentTypeAsync(
        QueryLatestContentsByContentType model, CancellationToken cancellationToken = default) {
        model.ThrowExceptionIfArgumentIsNull(nameof(model));

        var query = _set
            .Include(_=> _.Categories)
            .Include(_=> _.Tags)
            .Include(_=> _.ContentType)
            .Include(_=> _.Language)
            .Where(_ => _.WebsiteId == model.WebsiteId);
        if (model.ContentTypeId.HasValue)
        {
            query = query.Where(_ => _.ContentTypeId == model.ContentTypeId.Value);
        }

        if (model.ContentTypeName.IsNotNullOrEmpty())
        {
            query = query.Where(_ => _.ContentType.SystemName.ToUpper() == model.ContentTypeName.ToUpper());
        }

        return await query.OrderByDescending(_ => _.CreatedDate)
                            .Take(model.RecordsCount)
                            .ToListAsync(cancellationToken).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public async Task<Content?> GetByContentTypeAndSlugAsync(
        QueryContentByContentTypeAndSlug model, CancellationToken cancellationToken = default)
    {
        model.ThrowExceptionIfArgumentIsNull(nameof(model));

        var query = _set.Where(_ => _.WebsiteId == model.WebsiteId)
            .Where(_=> _.Slug.ToUpper() == model.Slug.ToUpper());
        
        if (model.ContentTypeId.HasValue)
        {
            query = query.Where(_ => _.ContentTypeId == model.ContentTypeId.Value);
        }

        if (model.ContentTypeName.IsNotNullOrEmpty())
        {
            query = query.Where(_ => _.ContentType.SystemName.ToUpper() == model.ContentTypeName.ToUpper());
        }

        if (model.LangId.HasValue)
        {
            query = query.Where(_ => _.LangId == model.LangId.Value);
        }

        return await query.Include(_ => _.Categories)
            .Include(_ => _.Language)
            .Include(_ => _.ContentType)
            .Include(_ => _.Tags)
            .FirstOrDefaultAsync(cancellationToken).ConfigureAwait(false);
    }

    public Task<IReadOnlyCollection<Content>> FilterAsync(QueryContentsFiltered model, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}