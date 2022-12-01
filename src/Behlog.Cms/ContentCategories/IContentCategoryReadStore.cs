using Behlog.Core;
using Behlog.Cms.Domain;

namespace Behlog.Cms.Store;


public interface IContentCategoryReadStore : IBehlogReadStore<ContentCategory, Guid>
{


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
}