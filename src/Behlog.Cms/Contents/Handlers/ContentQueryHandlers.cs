using Behlog.Cms.Domain;
using Behlog.Cms.Models;
using Behlog.Cms.Query;
using Behlog.Core;
using Behlog.Core.Models;
using Behlog.Extensions;
using Idyfa.Core.Contracts;

namespace Behlog.Cms.Handlers;


public class ContentQueryHandlers :
    IBehlogQueryHandler<QueryContentById, ContentResult>,
    IBehlogQueryHandler<QueryContentBySlug, ContentResult>,
    IBehlogQueryHandler<QueryLatestContentsByWebsite, IReadOnlyCollection<ContentResult>>,
    IBehlogQueryHandler<QueryLatestContentsByContentType, IReadOnlyCollection<ContentResult>>,
    IBehlogQueryHandler<QueryContentByContentTypeAndSlug, ContentResult>,
    IBehlogQueryHandler<QueryContentsFiltered, QueryResult<ContentResult>>
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
            .WithLikesCount(await _readStore.CountLikesAsync(content, cancellationToken));

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
            .WithLikesCount(await _readStore.CountLikesAsync(content, cancellationToken));

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
        
        var contents = await _readStore
            .GetLatestByWebsiteIdAsync(query.WebsiteId, query.RecordsCount, cancellationToken)
            .ConfigureAwait(false);
        
        var result = contents.Select(async _ => _.ToResult()
            .WithCategories(_.Categories?.ToList())
            .WithTags(_.Tags?.ToList())
            .WithLanguage(_.Language)
            .WithContentType(_.ContentType)
            .WithLikesCount(await _readStore.CountLikesAsync(_.Id, cancellationToken))
        ).ToList();

        return await Task.WhenAll(result);
    }

    public async Task<IReadOnlyCollection<ContentResult>> HandleAsync(
        QueryLatestContentsByContentType query, CancellationToken cancellationToken = default) 
    {
        query.ThrowExceptionIfArgumentIsNull(nameof(query));

        var contents = await _readStore.QueryAsync(query, cancellationToken).ConfigureAwait(false);
        
        var result = contents.Select(async _ => _.ToResult()
            .WithCategories(_.Categories?.ToList())
            .WithTags(_.Tags?.ToList())
            .WithLanguage(_.Language)
            .WithContentType(_.ContentType)
            .WithLikesCount(await _readStore.CountLikesAsync(_.Id, cancellationToken))
        ).ToList();

        return await Task.WhenAll(result);
    }

    public async Task<ContentResult> HandleAsync(
        QueryContentByContentTypeAndSlug query, CancellationToken cancellationToken = default)
    {
        query.ThrowExceptionIfArgumentIsNull(nameof(query));

        var content = await _readStore.QueryAsync(query, cancellationToken).ConfigureAwait(false);
        content.ThrowExceptionIfReferenceIsNull(nameof(content));

        return content!.ToResult()
            .WithFiles(content!.Files?.ToList())
            .WithCategories(content!.Categories?.ToList())
            .WithLanguage(content!.Language)
            .WithContentType(content!.ContentType)
            .WithTags(content!.Tags?.ToList());
    }

    public async Task<QueryResult<ContentResult>> HandleAsync(
        QueryContentsFiltered query, CancellationToken cancellationToken = default)
    {
        query.ThrowExceptionIfArgumentIsNull(nameof(query));

        var queryResult = await _readStore.FilterAsync(query, cancellationToken).ConfigureAwait(false);

        return QueryResult<ContentResult>
            .Create(queryResult.Results.Select(_ => _.ToResult()))
            .WithPageNumber(queryResult.PageNumber)
            .WithPageSize(queryResult.PageSize)
            .WithTotalCount(queryResult.TotalCount);
    }
}