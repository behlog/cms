using Behlog.Cms.Domain;
using Behlog.Cms.EntityFrameworkCore.Extensions;
using Behlog.Cms.Query;
using Behlog.Cms.Store;
using Behlog.Core.Models;
using Behlog.Extensions;
using Microsoft.EntityFrameworkCore;

namespace Behlog.Cms.EntityFrameworkCore.Stores;


public class FileUploadReadStore : BehlogEntityFrameworkCoreReadStore<FileUpload, Guid>, IFileUploadReadStore
{
    
    public FileUploadReadStore(IBehlogEntityFrameworkDbContext db) : base(db)
    {
    }

    /// <inheritdoc /> 
    public async Task<QueryResult<FileUpload>> GetFilesAsync(
        QueryFileUploadsByWebsiteId model, CancellationToken cancellationToken = default)
    {
        model.ThrowExceptionIfArgumentIsNull(nameof(model));

        var query = _set.Where(_ => _.WebsiteId == model.WebsiteId);
        
        return QueryResult<FileUpload>.Create(
            await query.SortBy(model.Options.OrderBy, model.Options.OrderDesc)
                        .ToListAsync(cancellationToken).ConfigureAwait(false))
            .WithPageNumber(model.Options.PageNumber)
            .WithPageSize(model.Options.PageSize)
            .WithTotalCount(await query.LongCountAsync(cancellationToken).ConfigureAwait(false));
        
    }
}