using Behlog.Core;
using Behlog.Cms.Domain;

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
        Guid websiteId, CancellationToken cancellationToken = default);
    
    /// <summary>
    /// Finds all the <see cref="ContentCategory"/> with the given <see cref="ContentType"/> Id.
    /// </summary>
    /// <param name="contentTypeId">ContentType Id</param>
    /// <returns>A read only list of ContentType's ContentCategories.</returns>
    Task<IReadOnlyCollection<ContentCategory>> FindByContentTypeAsync(
        Guid contentTypeId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Finds all the <see cref="ContentCategory"/> with specified ParentId.
    /// </summary>
    /// <param name="parentId"></param>
    /// <param name="cancellationToken"></param>
    /// <returns>A list of <see cref="ContentCategory"/>(s) with the same ParentId.</returns>
    Task<IReadOnlyCollection<ContentCategory>> FindByParentIdAsync(
        Guid? parentId, CancellationToken cancellationToken = default);
}