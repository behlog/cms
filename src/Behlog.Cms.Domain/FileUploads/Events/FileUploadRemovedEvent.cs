using Behlog.Core;

namespace Behlog.Cms.Events;

public class FileUploadRemovedEvent : IBehlogEvent
{
    public FileUploadRemovedEvent(Guid id, string fileName, string filePath)
    {
        Id = id;
        FileName = fileName;
        FilePath = filePath;
    }
    
    public Guid Id { get; }
    public string FileName { get; }
    public string FilePath { get; }
}