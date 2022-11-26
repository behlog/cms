using Behlog.Cms.Domain;
using Behlog.Cms.Store;
using Behlog.Core;

namespace Behlog.Cms.EntityFrameworkCore.Stores;

public class FileUploadWriteStore : BehlogWriteStore<FileUpload, Guid>, 
    IFileUploadWriteStore
{
    
    public FileUploadWriteStore(IBehlogEntityFrameworkDbContext db, IBehlogMediator mediator) 
        : base(db, mediator)
    {
    }
}