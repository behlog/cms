using Behlog.Core;
using Behlog.Cms.Domain;

namespace Behlog.Cms.Store;

public interface IFileUploadWriteStore : IBehlogWriteStore<FileUpload, Guid>
{
    
}