using Behlog.Cms.Models;
using Behlog.Cms.Query;
using Behlog.Cms.Store;
using Behlog.Core;
using Behlog.Core.Models;
using Behlog.Extensions;

namespace Behlog.Cms.Handlers;


public class FileUploadQueryHandlers :
    IBehlogQueryHandler<QueryFileUploadsByWebsiteId, QueryResult<FileUploadResult>>
{
    private readonly IFileUploadReadStore _readStore;


    public FileUploadQueryHandlers(IFileUploadReadStore readStore)
    {
        _readStore = readStore ?? throw new ArgumentNullException(nameof(readStore));
    }
    
    public async Task<QueryResult<FileUploadResult>> HandleAsync(
        QueryFileUploadsByWebsiteId query, CancellationToken cancellationToken = default)
    {
        query.ThrowExceptionIfArgumentIsNull(nameof(query));

        var files = await _readStore.GetFilesAsync(query, cancellationToken).ConfigureAwait(false);
        
        return QueryResult<FileUploadResult>.Create()
            .WithPageNumber(query.Options.PageNumber)
            .WithPageSize(query.Options.PageSize)
            .WithTotalCount(files.TotalCount)
            .WithResults(files.Results.Select(_=> _.ToResult()).ToList());
    }
}