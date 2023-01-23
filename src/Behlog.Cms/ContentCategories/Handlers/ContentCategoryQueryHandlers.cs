using Behlog.Core;
using Behlog.Cms.Query;
using Behlog.Cms.Store;
using Behlog.Cms.Models;
using Behlog.Extensions;

namespace Behlog.Cms.Handlers;


public class ContentCategoryQueryHandlers :
    IBehlogQueryHandler<QueryContentCategoryById, ContentCategoryResult>,
    IBehlogQueryHandler<QueryContentCategoryByParentId, IReadOnlyCollection<ContentCategoryResult>>,
    IBehlogQueryHandler<QueryWebsiteContentCategories, ContentCategoryListResult>,
    IBehlogQueryHandler<QueryContentCategoryByContentType, IReadOnlyCollection<ContentCategoryResult>>,
    IBehlogQueryHandler<QueryContentCategoriesFiltered, QueryResult<ContentCategoryResult>>
{
    private readonly IContentCategoryReadStore _readStore;

    public ContentCategoryQueryHandlers(IContentCategoryReadStore readStore)
    {
        _readStore = readStore ?? throw new ArgumentNullException(nameof(readStore));
    }

    public async Task<ContentCategoryResult> HandleAsync(
        QueryContentCategoryById query, CancellationToken cancellationToken = default)
    {
        query.ThrowExceptionIfArgumentIsNull(nameof(query));

        var category = await _readStore
            .FindByIdAsync(query.Id, cancellationToken).ConfigureAwait(false);
        category.ThrowExceptionIfReferenceIsNull(nameof(category));

        return await Task.FromResult(category.ToResult());
    }

    public async Task<IReadOnlyCollection<ContentCategoryResult>> HandleAsync(
        QueryContentCategoryByParentId query, CancellationToken cancellationToken = default)
    {
        query.ThrowExceptionIfArgumentIsNull(nameof(query));

        var categories = await _readStore.FindByParentIdAsync(
                query.LangId, query.ParentId, cancellationToken).ConfigureAwait(false);

        return await Task.FromResult(
            categories.Select(_ => _.ToResult()).ToList()
            );
    }

    
    public async Task<ContentCategoryListResult> HandleAsync(
        QueryWebsiteContentCategories query, CancellationToken cancellationToken = default)
    {
        query.ThrowExceptionIfArgumentIsNull(nameof(query));

        var categories = await _readStore.FindWebsiteContentCategoriesAsync(
            query.WebsiteId, query.LangId, query.ContentTypeId, cancellationToken).ConfigureAwait(false);
        categories.ThrowExceptionIfReferenceIsNull(nameof(categories));

        var result = new ContentCategoryListResult(
            categories.Select(_ => _.ToResult()).ToList());
        return await Task.FromResult(result);
    }

    public async Task<IReadOnlyCollection<ContentCategoryResult>> HandleAsync(
        QueryContentCategoryByContentType query, CancellationToken cancellationToken = default)
    {
        query.ThrowExceptionIfArgumentIsNull(nameof(query));

        var categories = await _readStore.FindByContentTypeAsync(query, cancellationToken).ConfigureAwait(false);
        categories.ThrowExceptionIfReferenceIsNull(nameof(categories));

        return await Task.FromResult(
            categories.Select(_ => _.ToResult()).ToList()
            );
    }


    public async Task<QueryResult<ContentCategoryResult>> HandleAsync(
        QueryContentCategoriesFiltered query, CancellationToken cancellationToken = default)
    {
        query.ThrowExceptionIfArgumentIsNull(nameof(query));

        var result = await _readStore.QueryAsync(query, cancellationToken).ConfigureAwait(false);
        return QueryResult<ContentCategoryResult>
            .Create(result.Results.Select(_=> _.ToResult()))
            .WithPageNumber(result.PageNumber)
            .WithPageSize(result.PageSize)
            .WithTotalCount(result.TotalCount);
    }
}