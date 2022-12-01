using Behlog.Cms.Models;
using Behlog.Cms.Query;
using Behlog.Cms.Store;using Behlog.Core;

namespace Behlog.Cms.Handlers;


public class ContentCategoryQueryHandlers :
    IBehlogQueryHandler<QueryContentCategoryById, ContentCategoryResult>,
    IBehlogQueryHandler<QueryContentCategoryByParentId, IReadOnlyCollection<ContentCategoryResult>>,
    IBehlogQueryHandler<QueryWebsiteContentCategories, ContentCategoryListResult>
{
    public Task<ContentCategoryResult> HandleAsync(
        QueryContentCategoryById query, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<IReadOnlyCollection<ContentCategoryResult>> HandleAsync(
        QueryContentCategoryByParentId query, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<ContentCategoryListResult> HandleAsync(
        QueryWebsiteContentCategories query, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}