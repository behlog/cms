namespace Behlog.Cms.Handlers;


public class FileUploadQueryHandlers :
    IBehlogQueryHandler<QueryFileUploadsByWebsiteId, QueryResult<FileUploadResult>>,
    IBehlogQueryHandler<QueryFileUploadById, FileUploadResult>
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

    public async Task<FileUploadResult> HandleAsync(
        QueryFileUploadById query, CancellationToken cancellationToken = new CancellationToken())
    {
        query.ThrowExceptionIfArgumentIsNull(nameof(query));

        var file = await _readStore.GetByIdAsync(query.Id, cancellationToken).ConfigureAwait(false);
        file.ThrowExceptionIfReferenceIsNull($"[{nameof(FileUpload)}] cannot found with Id: '{query.Id}'");

        return await Task.FromResult(file.ToResult());
    }
}