using Behlog.Core;

namespace Behlog.Cms.Events;

public class FileUploadAttachedEvent : IBehlogEvent
{
    public FileUploadAttachedEvent(
        Guid id, Guid contentId, 
        string fileName, string contentTitle)
    {
        Id = id;
        ContentId = contentId;
        FileName = fileName;
        ContentTitle = contentTitle;
    }


    public Guid Id { get; }
    public Guid ContentId { get; }
    public string FileName { get; }
    public string ContentTitle { get; }
}