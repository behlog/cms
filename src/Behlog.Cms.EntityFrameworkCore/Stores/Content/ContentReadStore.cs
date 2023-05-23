using Behlog.Cms.Domain;
using Behlog.Cms.Query;
using Behlog.Extensions;
using Behlog.Core.Models;
using Microsoft.EntityFrameworkCore;
using Behlog.Cms.EntityFrameworkCore.Extensions;
using Behlog.Cms.Models;
using Microsoft.Extensions.Options;

namespace Behlog.Cms.EntityFrameworkCore.Stores;


public class ContentReadStore : BehlogEntityFrameworkCoreReadStore<Content, Guid>, IContentReadStore
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
            .Include(_=> _.Components)
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
        var content = await _set.FindAsync(id).ConfigureAwait(false);
        return await CountLikesAsync(content, cancellationToken);
    }


    public async Task<int> CountLikesAsync(Content content, CancellationToken cancellationToken = default) {
        content.ThrowExceptionIfArgumentIsNull(nameof(content));
        var totalLikes = await _db.Entry(content)
            .Collection(_ => _.Likes)
            .Query()
            .CountAsync(cancellationToken).ConfigureAwait(false);
        return totalLikes;
    }


    /// <inheritdoc />
    public async Task<Content?> GetBySlugAsync(
        Guid websiteId, string slug, CancellationToken cancellationToken = default)
    {
        return await _set
            .Include(_ => _.Categories)
            .Include(_ => _.Components)
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
    public async Task<IReadOnlyCollection<Content>> QueryAsync(
        QueryLatestContentsByContentType model, CancellationToken cancellationToken = default) {
        model.ThrowExceptionIfArgumentIsNull(nameof(model));

        var query = _set
            .Include(_=> _.Files)
            .ThenInclude(_=> _.File)
            .Include(_=> _.Categories)
            .ThenInclude(_=> _.Category)
            .Include(_=> _.Tags)
            .ThenInclude(_=> _.Tag)
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
    public async Task<Content?> QueryAsync(
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

        return await query
            .Include(_ => _.Categories)
            .ThenInclude(_=> _.Category)
            .Include(_ => _.Language)
            .Include(_ => _.ContentType)
            .Include(_ => _.Tags)
            .ThenInclude(_=> _.Tag)
            .Include(_ => _.Files)
            .ThenInclude(_=> _.File)
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
                        .AddConditionIfNotNull(model.Status, _=> _.Status == model.Status)
                        .AddConditionIfNotNull(model.Search,
                            _=> _.Title.ToUpper().Contains(model.Search!) ||
                                _.Body!.ToUpper().Contains(model.Search!) ||
                                _.Summary!.ToUpper().Contains(model.Search!) ||
                                _.AltTitle!.ToUpper().Contains(model.Search!));

        return QueryResult<Content>.Create()
            .WithPageNumber(model.Options.PageNumber)
            .WithPageSize(model.Options.PageSize)
            .WithTotalCount(await query.LongCountAsync(cancellationToken).ConfigureAwait(false))
            .WithResults(await query
                .Include(_=> _.Categories)
                .ThenInclude(_=> _.Category)
                .Include(_=> _.Tags)
                .ThenInclude(_=> _.Tag)
                .Include(_=> _.ContentType)
                .Include(_=> _.Language)
                .Include(_=> _.Files)
                .ThenInclude(_=> _.File)
                .Include(_=> _.Meta)
                .SortBy(model.Options.OrderBy, model.Options.OrderDesc)
                .Skip(model.Options.StartIndex)
                .Take(model.Options.PageSize)
                .ToListAsync(cancellationToken).ConfigureAwait(false));
    }

    /// <inheritdoc />
    public async Task<bool> ExistBySlugAsync(
        Guid websiteId, string slug, CancellationToken cancellationToken = default)
    {
        return await _set.AnyAsync(_ => _.WebsiteId == websiteId && _.Slug.ToUpper() == slug.ToUpper());
    }

    /// <inheritdoc />
    public async Task<QueryResult<Content>> QueryAsync(
        Guid websiteId, Guid langId, string contentTypeName, ContentStatusEnum status, 
        QueryOptions options, CancellationToken cancellationToken = default) {

        var query = _set.Where(_ => _.WebsiteId == websiteId)
                        .Where(_=> _.LangId == langId)
                        .Where(_=> _.ContentType.SystemName.ToUpper() == contentTypeName.ToUpper())
                        .Where(_=> _.ContentType.LangId == langId)
                        .Where(_=> _.Status == status);

        return QueryResult<Content>.Create()
            .WithPageNumber(options.PageNumber)
            .WithPageSize(options.PageSize)
            .WithTotalCount(await query.LongCountAsync(cancellationToken).ConfigureAwait(false))
            .WithResults(await query
                .Include(_ => _.Categories)
                .ThenInclude(_ => _.Category)
                .Include(_ => _.Tags)
                .ThenInclude(_ => _.Tag)
                .Include(_ => _.ContentType)
                .Include(_ => _.Language)
                .SortBy(options.OrderBy, options.OrderDesc)
                .Skip(options.StartIndex)
                .Take(options.PageSize)
                .ToListAsync(cancellationToken).ConfigureAwait(false)
            );
    }

    /// <inheritdoc /> 
    public async Task<QueryResult<Content>> QueryAsync(
        QueryContentByWebsiteAndContentType model, CancellationToken cancellationToken = default)
    {
        model.ThrowExceptionIfArgumentIsNull(nameof(model));

        var query = _set.Where(_ => _.WebsiteId == model.WebsiteId)
                        .Where(_ => _.LangId == model.LangId)
                        .Where(_ => _.ContentTypeId == model.ContentTypeId);

        if (model.Status.HasValue)
        {
            query = query.Where(_ => _.Status == model.Status.Value);
        }

        return QueryResult<Content>.Create()
            .WithPageNumber(model.Options.PageNumber)
            .WithPageSize(model.Options.PageSize)
            .WithTotalCount(await query.LongCountAsync(cancellationToken).ConfigureAwait(false))
            .WithResults(await query
                .Include(_ => _.Categories)
                .ThenInclude(_ => _.Category)
                .Include(_ => _.ContentType)
                .Include(_ => _.Language)
                // .Include(_=> _.AuthorUser)
                .SortBy(model.Options.OrderBy, model.Options.OrderDesc)
                .Skip(model.Options.StartIndex)
                .Take(model.Options.PageSize)
                .ToListAsync(cancellationToken).ConfigureAwait(false)
            );
    }
}