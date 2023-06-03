using Behlog.Core;
using Behlog.Cms.Domain;
using Behlog.Cms.Query;
using Behlog.Core.Models;

namespace Behlog.Cms.Store;

public interface IFileUploadReadStore : IBehlogReadStore<FileUpload, Guid>
{

    /// <summary>
    /// Get Uploaded files by WebsiteId.
    /// </summary>
    /// <param name="websiteId"></param>
    /// <param name="cancellationToken"></param>
    Task<QueryResult<FileUpload>> GetFilesAsync(
        QueryFileUploadsByWebsiteId model, CancellationToken cancellationToken = default);


    Task<FileUpload?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
}