using Behlog.Cms.Domain;
using Behlog.Core.Domain;

namespace Behlog.Cms.Events;

public class FileUpdatedEvent : BehlogDomainEvent
{
    public FileUpdatedEvent(
        Guid id, string title, string filePath, string fileName, string alternateFilePath, 
        string extension, long fileSize, string altTitle, string url, FileUploadStatus status, 
        string description, string createdByUserId, string updatedByUserId, 
        string createdByIp, string updatedByIp, DateTime? lastStatusChangedOn, 
        DateTime createdDate)
    {
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
        CreatedByUserId = createdByUserId;
        UpdatedByUserId = updatedByUserId;
        CreatedByIp = createdByIp;
        UpdatedByIp = updatedByIp;
        LastStatusChangedOn = lastStatusChangedOn;
        CreatedDate = createdDate;
        LastStatusChangedOn = lastStatusChangedOn;
        CreatedDate = createdDate;
    }
    
    public Guid Id { get; }
    public string Title { get; }
    public string FilePath { get; }
    public string FileName { get; }
    public string AlternateFilePath { get; }
    public string Extension { get; }
    public long FileSize { get; }
    public string AltTitle { get; }
    public string Url { get; }
    public FileUploadStatus Status { get; }
    public DateTime? LastStatusChangedOn { get; }
    public string Description { get; }
    public DateTime CreatedDate { get; }
    public string CreatedByUserId { get; }
    public string UpdatedByUserId { get; }
    public string CreatedByIp { get; }
    public string UpdatedByIp { get; }
}