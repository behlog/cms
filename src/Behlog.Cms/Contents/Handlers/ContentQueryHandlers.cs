using Behlog.Cms.Domain;
using Behlog.Cms.Models;
using Behlog.Cms.Query;
using Behlog.Core;
using Behlog.Extensions;
using Idyfa.Core.Contracts;

namespace Behlog.Cms.Handlers;

public class ContentQueryHandlers :
    IBehlogQueryHandler<QueryContentById, ContentResult>,
    IBehlogQueryHandler<QueryContentBySlug, ContentResult>,
    IBehlogQueryHandler<QueryLatestContentsByWebsite, IReadOnlyCollection<ContentResult>>,
    IBehlogQueryHandler<QueryLatestContentsByContentType, IReadOnlyCollection<ContentResult>>
{
    private readonly IContentReadStore _readStore;
    private readonly IIdyfaUserRepository _userRepo;
    
    public ContentQueryHandlers(
        IContentReadStore readStore, IIdyfaUserRepository userRepo)
    {
        _readStore = readStore ?? throw new ArgumentNullException(nameof(readStore));
        _userRepo = userRepo ?? throw new ArgumentNullException(nameof(userRepo));
    }
    
    public async Task<ContentResult> HandleAsync(
        QueryContentById query, CancellationToken cancellationToken = default)
    {
        query.ThrowExceptionIfArgumentIsNull(nameof(query));

        var content = await _readStore.GetByIdAsync(query.Id, cancellationToken);
        content.ThrowExceptionIfReferenceIsNull(nameof(content));

        var result = content.ToResult()
            .WithCategories(content.Categories?.ToList())
            .WithFiles(content.Files?.ToList())
            .WithLanguage(content.Language)
            .WithMeta(content.Meta?.ToList())
            .WithTags(content.Tags?.ToList())
            .WithContentType(content.ContentType)
            .WithLikesCount(await _readStore.CountLikesAsync(content.Id, cancellationToken));

        var author = await _userRepo.FindByIdAsync(
            content.AuthorUserId, cancellationToken).ConfigureAwait(false);
        if (author != null)
        {
            result.WithAuthor(author.UserName, author.DisplayName);
        }
        
        return await Task.FromResult(result);
    }

    public async Task<ContentResult> HandleAsync(
        QueryContentBySlug query, CancellationToken cancellationToken = default)
    {
        query.ThrowExceptionIfArgumentIsNull(nameof(query));

        if (query.WebsiteId == default)
            throw new BehlogInvalidEntityIdException(nameof(Website));
        
        if (query.Slug.IsNullOrEmptySpace())
            throw new ArgumentNullException(nameof(query.Slug));

        var normalizedSlug = query.Slug.CorrectYeKe().ToUpper().Trim();
        var content = await _readStore.GetBySlugAsync(query.WebsiteId, normalizedSlug, cancellationToken);
        content.ThrowExceptionIfReferenceIsNull(nameof(content));
        
        var result = content.ToResult()
            .WithCategories(content.Categories?.ToList())
            .WithFiles(content.Files?.ToList())
            .WithLanguage(content.Language)
            .WithMeta(content.Meta?.ToList())
            .WithTags(content.Tags?.ToList())
            .WithContentType(content.ContentType)
            .WithLikesCount(await _readStore.CountLikesAsync(content.Id, cancellationToken));

        var author = await _userRepo.FindByIdAsync(
            content.AuthorUserId, cancellationToken).ConfigureAwait(false);
        if (author != null)
        {
            result.WithAuthor(author.UserName, author.DisplayName);
        }
        
        return await Task.FromResult(result);
    }

    public async Task<IReadOnlyCollection<ContentResult>> HandleAsync(
        QueryLatestContentsByWebsite query, CancellationToken cancellationToken = default) 
    {
        query.ThrowExceptionIfArgumentIsNull(nameof(query));
        
        var contents = await _readStore.GetLatestByWebsiteId(query.WebsiteId, query.RecordsCount);
        var result = 
    }

    public Task<IReadOnlyCollection<ContentResult>> HandleAsync(
        QueryLatestContentsByContentType query, CancellationToken cancellationToken = default) {
        throw new NotImplementedException();
    }

}