using Behlog.Cms.Domain;
using Behlog.Cms.Store;

namespace Behlog.Cms.EntityFrameworkCore.Stores;

public class FileUploadWriteStore : BehlogWriteStore<FileUpload, Guid>, 
    IFileUploadWriteStore
{
    
    public FileUploadWriteStore(IBehlogEntityFrameworkDbContext db) : base(db)
    {
    }
}