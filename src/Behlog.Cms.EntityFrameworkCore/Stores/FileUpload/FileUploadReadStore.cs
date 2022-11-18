using Behlog.Cms.Domain;
using Behlog.Cms.Store;

namespace Behlog.Cms.EntityFrameworkCore.Stores;


public class FileUploadReadStore : BehlogReadStore<FileUpload, Guid>, IFileUploadReadStore
{
    
    public FileUploadReadStore(IBehlogEntityFrameworkDbContext db) : base(db)
    {
    }
}