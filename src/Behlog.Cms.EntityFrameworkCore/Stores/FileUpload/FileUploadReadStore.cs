using Behlog.Cms.Domain;
using Behlog.Cms.EntityFrameworkCore.Extensions;
using Behlog.Cms.Query;
using Behlog.Cms.Store;
using Behlog.Core.Models;
using Behlog.Extensions;
using Microsoft.EntityFrameworkCore;

namespace Behlog.Cms.EntityFrameworkCore.Stores;


public class FileUploadReadStore : BehlogReadStore<FileUpload, Guid>, IFileUploadReadStore
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
            await query.SortBy(model.Filter.OrderBy, model.Filter.OrderDesc)
                        .ToListAsync(cancellationToken).ConfigureAwait(false))
            .WithPageNumber(model.Filter.PageNumber)
            .WithPageSize(model.Filter.PageSize)
            .WithTotalCount(await query.LongCountAsync(cancellationToken).ConfigureAwait(false));
        
    }
}