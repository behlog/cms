namespace Behlog.Cms.Models;

public class FileUploadResult
{
    
    public Guid Id { get; set; }
    public Guid WebsiteId { get; set; }
    public string? Title { get; set; }
    public string FilePath { get; set; }
    public string FileUrl { get; set; }
    public string FileName { get; set; }
    public string? AlternateFilePath { get; set; }
    public string? AltFileUrl { get; set; }
    public string? Extension { get; set; }
    public long FileSize { get; set; }
    public long? AltFileSize { get; set; }
    public string? AltTitle { get; set; }
    public string? Url { get; set; }
    public FileUploadStatus Status { get; set; }
    public FileTypeEnum FileType { get; set; }
    public string? Description { get; set; }
    public DateTime? LastStatusChangedOn { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime? LastUpdated { get; set; }
    public string? CreatedByUserId { get; set; }
    public string? LastUpdatedByUserId { get; set; }
    public string? CreatedByIp { get; set; }
    public string? LastUpdatedByIp { get; set; }
}