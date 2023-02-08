using Behlog.Cms.Store;
using Behlog.Cms.Query;
using Behlog.Cms.Domain;
using Behlog.Extensions;
using Microsoft.EntityFrameworkCore;
using Behlog.Cms.EntityFrameworkCore.Extensions;
using Behlog.Core;
using Behlog.Core.Models;

namespace Behlog.Cms.EntityFrameworkCore.Stores;


public class ContentCategoryReadStore : BehlogEntityFrameworkCoreReadStore<ContentCategory, Guid>, 
    IContentCategoryReadStore
{
    private readonly IQueryable<ContentCategory> _categories;
    
    public ContentCategoryReadStore(IBehlogEntityFrameworkDbContext db) 
        : base(db)
    {
        _categories = _set.Where(_=> _.Status != EntityStatus.Deleted);
    }
    
    /// <inheritdoc />
    public async Task<ContentCategory> FindByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return (await _set.Include(_ => _.Language)
            .Include(_ => _.ContentType)
            .Include(_ => _.Website)
            .SingleOrDefaultAsync(_ => _.Id == id, cancellationToken).ConfigureAwait(false))!;
    }

    /// <inheritdoc /> 
    public async Task<IReadOnlyCollection<ContentCategory>> FindWebsiteContentCategoriesAsync(
        Guid websiteId, Guid langId, Guid? contentTypeId, CancellationToken cancellationToken = default)
    {
        return await _set.Where(_ => _.WebsiteId == websiteId)
                         .Where(_ => _.LangId == langId)
                         .AddConditionIfNotNull(contentTypeId,
                            _ => _.ContentTypeId == contentTypeId!.Value)
                         .ToListAsync(cancellationToken).ConfigureAwait(false);
    }

    
    /// <inheritdoc /> 
    public async Task<IReadOnlyCollection<ContentCategory>> FindByContentTypeAsync(
        QueryContentCategoryByContentType model, CancellationToken cancellationToken = default)
    {
        model.ThrowExceptionIfArgumentIsNull(nameof(model));

        return await _set.Include(_=> _.ContentType)
                         .AddConditionIfNotNull(model.LangId, 
                            _ => _.LangId == model.LangId!.Value)
                         .AddConditionIfNotNull(model.ContentTypeId, 
                            _ => _.ContentTypeId == model.ContentTypeId!.Value)
                         .AddConditionIfNotNull(model.ContentTypeName,
                            _ => _.ContentType.SystemName.ToUpper() == model.ContentTypeName!.ToUpper())
                         .ToListAsync(cancellationToken).ConfigureAwait(false);
    }
    
    /// <inheritdoc /> 
    public async Task<IReadOnlyCollection<ContentCategory>> FindByParentIdAsync(
        Guid langId, Guid? parentId, CancellationToken cancellationToken = default)
    {
        return await _set.Where(_ => _.LangId == langId && _.ParentId == parentId)
            .ToListAsync(cancellationToken).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public async Task<QueryResult<ContentCategory>> QueryAsync(
        QueryContentCategoriesFiltered model, CancellationToken cancellationToken = default)
    {
        model.ThrowExceptionIfArgumentIsNull(nameof(model));

        var query = _set
            .Include(_ => _.Language)
            .Include(_ => _.ContentType)
            .Where(_ => _.WebsiteId == model.WebsiteId)
            .Where(_ => _.LangId == model.LangId)
            .Where(_ => _.ContentTypeId == model.ContentTypeId);

        if (model.Status.HasValue)
        {
            query = query.Where(_ => _.Status == model.Status.Value);
        }

        if (model.Options.Search.IsNotNullOrEmpty())
        {
            model.Options.Search = model.Options.Search.ToUpper();
            query = query.Where(_ => _.Title.ToUpper().Contains(model.Options.Search) ||
                                     _.AltTitle.ToUpper().Contains(model.Options.Search) ||
                                     _.Slug.ToUpper().Contains(model.Options.Search));
        }

        return QueryResult<ContentCategory>.Create()
            .WithPageNumber(model.Options.PageNumber)
            .WithPageSize(model.Options.PageSize)
            .WithTotalCount(await query.LongCountAsync(cancellationToken).ConfigureAwait(false))
            .WithResults(await query
                .SortBy(model.Options.OrderBy, model.Options.OrderDesc)
                .Skip(model.Options.StartIndex)
                .Take(model.Options.PageSize)
                .ToListAsync(cancellationToken).ConfigureAwait(false)
            );
    }

    public async Task<bool> ExistByTitleAsync(
        Guid websiteId, Guid contentTypeId, Guid contentCategoryId, string title)
    {
        if (title.IsNullOrEmpty()) throw new ArgumentNullException(nameof(title));
        
        var normalizedTitle = title.CorrectYeKe().Trim();
        return await _categories
            .AnyAsync(_ => _.WebsiteId == websiteId &&
                            _.ContentTypeId == contentTypeId &&
                            _.Id != contentCategoryId &&
                            _.Title.ToUpper() == normalizedTitle);
    }

    public async Task<bool> ExistBySlugAsync(
        Guid websiteId, Guid contentTypeId, Guid contentCategoryId, string slug)
    {
        if (slug.IsNullOrEmpty()) throw new ArgumentNullException(nameof(slug));
        
        var normalizedSlug = slug.CorrectYeKe().Trim();
        return await _categories
            .AnyAsync(_ => _.WebsiteId == websiteId &&
                           _.ContentTypeId == contentTypeId &&
                           _.Id != contentCategoryId &&
                           _.Slug.ToUpper() == normalizedSlug);
    }
}