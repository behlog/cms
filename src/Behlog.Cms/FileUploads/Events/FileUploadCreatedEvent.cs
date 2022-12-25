using Behlog.Cms.Domain;
using Behlog.Core.Domain;

namespace Behlog.Cms.Events;

public class FileUploadCreatedEvent : BehlogDomainEvent
{
    public FileUploadCreatedEvent(
        Guid id, string title, string filePath, string fileName, string alternateFilePath, 
        string extension, long fileSize, string altTitle, string url, FileUploadStatusEnum status, 
        string description, DateTime createdDate, string createdByUserId, 
        string createdByIp)
    {
        Id = id;
        Title = title;
        FilePath = filePath;
        FileName = fileName;
        AlternateFilePath = alternateFilePath;
        Extension = extension;
        FileSize = fileSize;
        AltTitle = altTitle;
        Url = url;
        Status = status;
        Description = description;
        CreatedDate = createdDate;
        CreatedByUserId = createdByUserId;
        CreatedByIp = createdByIp;
    }
    
    public Guid Id { get; }
    public string Title { get; }
    public string FileName { get; }
    public string FilePath { get; }
    public string AlternateFilePath { get; }
    public string Extension { get; }
    public long FileSize { get; }
    public string AltTitle { get; }
    public string Url { get; }
    public FileUploadStatusEnum Status { get; }
    public string Description { get; }
    public DateTime CreatedDate { get; }
    public string CreatedByUserId { get; }
    public string CreatedByIp { get; }
}