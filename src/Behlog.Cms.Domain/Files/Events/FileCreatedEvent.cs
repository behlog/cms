using Behlog.Cms.Domain;
using Behlog.Core.Domain;

namespace Behlog.Cms.Events;

public class FileCreatedEvent : BehlogDomainEvent
{
    public FileCreatedEvent(
        Guid id, string title, string filePath, string alternateFilePath, 
        string extension, string altTitle, string url, FileStatus status, 
        string description, DateTime createdDate, string createdByUserId, 
        string createdByIp)
    {
        Id = id;
        Title = title;
        FilePath = filePath;
        AlternateFilePath = alternateFilePath;
        Extension = extension;
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
    public string FilePath { get; }
    public string AlternateFilePath { get; }
    public string Extension { get; }
    public string AltTitle { get; }
    public string Url { get; }
    public FileStatus Status { get; }
    public string Description { get; }
    public DateTime CreatedDate { get; }
    public string CreatedByUserId { get; }
    public string CreatedByIp { get; }
}