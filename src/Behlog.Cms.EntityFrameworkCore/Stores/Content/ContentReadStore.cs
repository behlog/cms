using Behlog.Cms.Domain;
using Behlog.Cms.Query;
using Behlog.Extensions;
using Behlog.Core.Models;
using Microsoft.EntityFrameworkCore;
using Behlog.Cms.EntityFrameworkCore.Extensions;

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
            .Where(_ => _.WebsiteId == model.WebsiteId)
            .AddConditionIfNotNull(model.ContentTypeId,
                _ => _.ContentTypeId == model.ContentTypeId!.Value)
            .AddConditionIfNotNull(model.ContentTypeName,
                _ => _.ContentType.SystemName.ToUpper() == model.ContentTypeName!.ToUpper());

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

    /// <inheritdoc />
    public async Task<QueryResult<Content>> FilterAsync(
        QueryContentsFiltered model, CancellationToken cancellationToken = default)
    {
        model.ThrowExceptionIfArgumentIsNull(nameof(model));
        
        var query = _set.Where(_ => _.WebsiteId == model.WebsiteId)
                        .AddConditionIfNotNull(model.LangId, _ => _.LangId == model.LangId!.Value)
                        .AddConditionIfNotNull(model.ContentTypeId, 
                            _=> _.ContentTypeId == model.ContentTypeId!.Value)
                        .AddConditionIfNotNull(model.ContentTypeName, 
                            _=> _.ContentType.SystemName.ToUpper() == model.ContentTypeName!.ToUpper())
                        .AddConditionIfNotNull(model.AuthorUserId,
                            _=> _.AuthorUserId == model.AuthorUserId)
                        .AddConditionIfNotNull(model.Title,
                            _=> _.Title.ToUpper().Contains(model.Title!))
                        .AddConditionIfNotNull(model.Status,
                            _=> _.Status.Id == model.Status!.Id)
                        .AddConditionIfNotNull(model.Search,
                            _=> _.Title.ToUpper().Contains(model.Search!) ||
                                _.Body!.ToUpper().Contains(model.Search!) ||
                                _.Summary!.ToUpper().Contains(model.Search!) ||
                                _.AltTitle!.ToUpper().Contains(model.Search!));

        return QueryResult<Content>.Create()
            .WithPageNumber(model.Filter.PageNumber)
            .WithPageSize(model.Filter.PageSize)
            .WithTotalCount(await query.LongCountAsync(cancellationToken).ConfigureAwait(false))
            .WithResults(await query
                .Include(_=> _.Categories)
                .Include(_=> _.Tags)
                .Include(_=> _.ContentType)
                .Include(_=> _.Language)
                .SortBy(model.Filter.OrderBy, model.Filter.OrderDesc)
                .Skip(model.Filter.StartIndex)
                .Take(model.Filter.PageSize)
                .ToListAsync(cancellationToken).ConfigureAwait(false));
    }
    
}