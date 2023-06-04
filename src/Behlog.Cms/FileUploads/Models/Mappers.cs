namespace Behlog.Cms.Models;

public static class FileUploadMappers
{

    public static FileUploadResult ToResult(this FileUpload fileUpload)
    {
        fileUpload.ThrowExceptionIfArgumentIsNull(nameof(fileUpload));
        return new FileUploadResult
        {
            Id = fileUpload.Id,
            WebsiteId = fileUpload.WebsiteId,
            Title = fileUpload.Title,
            FileName = fileUpload.FileName,
            FileUrl = fileUpload.FileUrl,
            Extension = fileUpload.Extension,
            Status = fileUpload.Status,
            FileType = fileUpload.FileType,
            Url = fileUpload.Url,
            AltTitle = fileUpload.AltTitle,
            Description = fileUpload.Description,
            LastStatusChangedOn = fileUpload.LastStatusChangedOn,
            CreatedDate = fileUpload.CreatedDate,
            FilePath = fileUpload.FilePath,
            FileSize = fileUpload.FileSize,
            LastUpdated = fileUpload.LastUpdated,
            AlternateFilePath = fileUpload.AlternateFilePath,
            AltFileUrl = fileUpload.AltFileUrl,
            AltFileSize = fileUpload.AltFileSize,
            CreatedByIp = fileUpload.CreatedByIp,
            CreatedByUserId = fileUpload.CreatedByUserId,
            LastUpdatedByIp = fileUpload.LastUpdatedByIp,
            LastUpdatedByUserId = fileUpload.LastUpdatedByUserId
        };
    }
    
}