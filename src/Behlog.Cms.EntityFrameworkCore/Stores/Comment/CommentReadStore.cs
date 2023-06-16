using Behlog.Cms.Domain;
using Behlog.Cms.EntityFrameworkCore.Extensions;
using Behlog.Cms.Query;
using Behlog.Core.Models;
using Behlog.Extensions;
using Microsoft.EntityFrameworkCore;

namespace Behlog.Cms.EntityFrameworkCore.Stores;

public class CommentReadStore : BehlogEntityFrameworkCoreReadStore<Comment, Guid>, ICommentReadStore
{
    
    public CommentReadStore(IBehlogEntityFrameworkDbContext db) 
        : base(db)
    {
    }

    public async Task<QueryResult<Comment>> QueryAsync(
        QueryWebsiteComments model, CancellationToken cancellationToken = default)
    {
        model.ThrowExceptionIfArgumentIsNull(nameof(model));

        var query = _set.Where(_ => _.Content.WebsiteId == model.WebsiteId);

        if (model.Status != null)
        {
            query = query.Where(_ => _.Status == model.Status.Value);
        }

        if (model.Options.Search.IsNotNullOrEmpty())
        {
            var search = model.Options.Search.ToUpper().CorrectYeKe();
            query = query.Where(_ => _.Title.ToUpper().Contains(search) ||
                                     _.AuthorName.ToUpper().Contains(search) ||
                                     _.Email.ToUpper().Contains(search) ||
                                     _.WebUrl.ToUpper().Contains(search) ||
                                     _.Body.ToUpper().Contains(search));
        }

        return QueryResult<Comment>.Create()
            .WithPageNumber(model.Options.PageNumber)
            .WithPageSize(model.Options.PageSize)
            .WithTotalCount(await query.LongCountAsync(cancellationToken))
            .WithResults(await query
                .Include(_ => _.Content)
                .ThenInclude(_ => _.ContentType)
                .SortBy(model.Options.OrderBy, model.Options.OrderDesc)
                .Skip(model.Options.StartIndex)
                .Take(model.Options.PageSize)
                .ToListAsync(cancellationToken).ConfigureAwait(false)
            );
    }
}