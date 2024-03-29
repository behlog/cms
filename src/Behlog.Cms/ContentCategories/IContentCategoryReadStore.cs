using Behlog.Core;
using Behlog.Cms.Domain;
using Behlog.Cms.Query;

namespace Behlog.Cms.Store;


public interface IContentCategoryReadStore : IBehlogReadStore<ContentCategory, Guid>
{
    
    /// <summary>
    /// Finds a <see cref="ContentCategory"/> by it's Id.
    /// </summary>
    /// <returns></returns>
    Task<ContentCategory> FindByIdAsync(Guid id, CancellationToken cancellationToken = default);
    
    /// <summary>
    /// Finds all the <see cref="ContentCategory"/>(s) with the given <see cref="Website"/>'s Id.
    /// </summary>
    /// <returns>A list of <see cref="ContentCategory"/>(s) based on a <see cref="Website"/></returns>
    Task<IReadOnlyCollection<ContentCategory>> FindWebsiteContentCategoriesAsync(
        Guid websiteId, Guid langId, Guid? contentTypeId, CancellationToken cancellationToken = default);
    
    /// <summary>
    /// Finds all the <see cref="ContentCategory"/> with the given <see cref="ContentType"/> Id.
    /// </summary>
    /// <param name="contentTypeId">ContentType Id</param>
    /// <returns>A read only list of ContentType's ContentCategories.</returns>
    Task<IReadOnlyCollection<ContentCategory>> FindByContentTypeAsync(
        QueryContentCategoryByContentType model, CancellationToken cancellationToken = default);

    /// <summary>
    /// Finds all the <see cref="ContentCategory"/> with specified ParentId.
    /// </summary>
    /// <param name="parentId"></param>
    /// <param name="cancellationToken"></param>
    /// <returns>A list of <see cref="ContentCategory"/>(s) with the same ParentId.</returns>
    Task<IReadOnlyCollection<ContentCategory>> FindByParentIdAsync(
        Guid langId, Guid? parentId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Get <see cref="ContentCategory"/> based on filter model with pagination support.
    /// </summary>
    /// <param name="model"></param>
    /// <param name="cancellationToken"></param>
    Task<QueryResult<ContentCategory>> QueryAsync(
        QueryContentCategoriesFiltered model, CancellationToken cancellationToken = default);


    Task<bool> ExistByTitleAsync(
        Guid websiteId, Guid contentTypeId, Guid contentCategoryId, string title);


    Task<bool> ExistBySlugAsync(
        Guid websiteId, Guid contentTypeId, Guid contentCategoryId, string slug);
}