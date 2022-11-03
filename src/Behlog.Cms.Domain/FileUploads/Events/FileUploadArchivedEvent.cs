using Behlog.Core;

namespace Behlog.Cms.Events;

public class FileUploadArchivedEvent : IBehlogEvent
{
    public FileUploadArchivedEvent(Guid id, string fileName, string filePath)
    {
        Id = id;
        FileName = fileName;
        FilePath = filePath;
    }


    public Guid Id { get; }
    public string FileName { get; }
    public string FilePath { get; }
}