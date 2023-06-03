using Behlog.Cms.Query;
using Behlog.Cms.Store;
using Behlog.Cms.Domain;
using Behlog.Extensions;
using Behlog.Core.Models;
using Microsoft.EntityFrameworkCore;
using Behlog.Cms.EntityFrameworkCore.Extensions;

namespace Behlog.Cms.EntityFrameworkCore.Stores;


public class FileUploadReadStore : BehlogEntityFrameworkCoreReadStore<FileUpload, Guid>, IFileUploadReadStore
{
    private IQueryable<FileUpload> _files; 

    public FileUploadReadStore(IBehlogEntityFrameworkDbContext db) : base(db)
    {
        _files = _set.Where(_ => _.Status != FileUploadStatus.Deleted);
    }

    /// <inheritdoc /> 
    public async Task<QueryResult<FileUpload>> GetFilesAsync(
        QueryFileUploadsByWebsiteId model, CancellationToken cancellationToken = default)
    {
        model.ThrowExceptionIfArgumentIsNull(nameof(model));

        var query = _files.Where(_ => _.WebsiteId == model.WebsiteId);
        
        if (model.Status.HasValue)
        {
            query = query.Where(_ => _.Status == model.Status.Value);
        }

        if (model.FileType.HasValue)
        {
            query = query.Where(_ => _.FileType == model.FileType.Value);
        }
        
        if (model.Options.Search.IsNotNullOrEmpty())
        {
            var search = model.Options.Search.CorrectYeKe().ToUpper();
            query = query.Where(_ => _.Title.ToUpper().Contains(search) ||
                                     _.Url.ToUpper().Contains(search) ||
                                     _.AltTitle.ToUpper().Contains(search) ||
                                     _.FileName.ToUpper().Contains(search) ||
                                     _.AlternateFilePath.ToUpper().Contains(search) ||
                                     _.Description.ToUpper().Contains(search));
        }
        
        return QueryResult<FileUpload>.Create(
            await query.SortBy(model.Options.OrderBy, model.Options.OrderDesc)
                        .ToListAsync(cancellationToken).ConfigureAwait(false))
            .WithPageNumber(model.Options.PageNumber)
            .WithPageSize(model.Options.PageSize)
            .WithTotalCount(await query.LongCountAsync(cancellationToken).ConfigureAwait(false));
    }

    public async Task<FileUpload?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _set.FirstOrDefaultAsync(_ => _.Id == id, cancellationToken).ConfigureAwait(false);
    }
}