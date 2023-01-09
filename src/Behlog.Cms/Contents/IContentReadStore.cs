using Behlog.Core;
using Behlog.Cms.Query;
using Behlog.Core.Models;

namespace Behlog.Cms.Domain;

/// <summary>
/// ReadStore for querying the <see cref="Content"/>.
/// </summary>
public interface IContentReadStore : IBehlogReadStore<Content, Guid>
{

    Task<Content?> GetByIdAsync(
        Guid id, CancellationToken cancellationToken = default);
    
    /// <summary>
    /// Gets total count of <see cref="ContentLike"/> for a <see cref="Content"/>
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<int> CountLikesAsync(
        Guid id, CancellationToken cancellationToken = default);


    Task<int> CountLikesAsync(
        Content content, CancellationToken cancellationToken = default);
    
    /// <summary>
    /// Get Contents with it's Categories, Blocks, Components, Files, Language, Meta and ContentType.
    /// </summary>
    /// <param name="websiteId"></param>
    /// <param name="slug"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<Content?> GetBySlugAsync(
        Guid websiteId, string slug, CancellationToken cancellationToken = default);

    /// <summary>
    /// Get latest Contents with it's Categories, Tags, ContentType and Language.
    /// </summary>
    /// <param name="websiteId"></param>
    /// <param name="take">Number of records to fetch.</param>
    /// <returns></returns>
    Task<IReadOnlyCollection<Content>> GetLatestByWebsiteIdAsync(
        Guid websiteId, int take = 10, CancellationToken cancellationToken = default);

    /// <summary>
    /// Get latest Contents by ContentType and Website.
    /// Includes: Categories, Tags, ContentType and Language.
    /// </summary>
    /// <param name="model">The Query params.</param>
    /// <returns></returns>
    Task<IReadOnlyCollection<Content>> QueryAsync(
        QueryLatestContentsByContentType model, CancellationToken cancellationToken = default);


    /// <summary>
    /// Get Content by ContentType and Slug.
    /// Includes: Categories, Blocks, Components, Files, Language, Meta and ContentType.
    /// </summary>
    /// <param name="model"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<Content?> QueryAsync(
        QueryContentByContentTypeAndSlug model, CancellationToken cancellationToken = default);


    /// <summary>
    /// Filter Contents and returns the results with pagination support.
    /// Includes: Categories, Tags, ContentType and Language. 
    /// </summary>
    /// <returns></returns>
    Task<QueryResult<Content>> FilterAsync(
        QueryContentsFiltered model, CancellationToken cancellationToken = default);

    /// <summary>
    /// Check if any <see cref="Content"/> exists within the Website with the given slug.
    /// </summary>
    /// <param name="websiteId">WebsiteId</param>
    /// <param name="slug">Slug to check</param>
    /// <returns>True if exists, otherwise false.</returns>
    Task<bool> ExistBySlugAsync(
        Guid websiteId, string slug, CancellationToken cancellationToken = default);

    /// <summary>
    /// Get Published Contents by ContentTypeName, Website and Status.
    /// Includes (Tags, Categories, ContentType and Language)
    /// </summary>
    /// <param name="websiteId"></param>
    /// <param name="contentTypeName"></param>
    /// <param name="status"></param>
    /// <param name="options"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<QueryResult<Content>> QueryAsync(
        Guid websiteId, Guid langId, string contentTypeName, ContentStatusEnum status, 
        QueryOptions options, CancellationToken cancellationToken = default);
}