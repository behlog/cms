using Behlog.Core;

namespace Behlog.Cms.Events;

public class FileUploadSoftDeletedEvent : IBehlogEvent
{
    public FileUploadSoftDeletedEvent(Guid fileUploadId)
    {
        Id = fileUploadId;
    }
    
    public Guid Id { get; }
}