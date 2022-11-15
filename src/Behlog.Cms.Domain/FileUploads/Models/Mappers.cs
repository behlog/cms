using Behlog.Cms.Domain;
using Behlog.Core;
using Behlog.Core.Validations;
using Behlog.Extensions;

namespace Behlog.Cms.Models;

public static class FileUploadMappers
{

    public static FileUploadResult ToResult(this FileUpload fileUpload)
    {
        fileUpload.ThrowExceptionIfArgumentIsNull(nameof(fileUpload));
        return new FileUploadResult
        {
            Id = fileUpload.Id,
            Title = fileUpload.Title,
            FileName = fileUpload.FileName,
            Extension = fileUpload.Extension,
            Status = fileUpload.Status,
            Url = fileUpload.Url,
            AltTitle = fileUpload.AltTitle,
            Description = fileUpload.Description,
            CreatedDate = fileUpload.CreatedDate,
            FilePath = fileUpload.FilePath,
            FileSize = fileUpload.FileSize,
            LastUpdated = fileUpload.LastUpdated,
            AlternateFilePath = fileUpload.AlternateFilePath,
            CreatedByIp = fileUpload.CreatedByIp,
            CreatedByUserId = fileUpload.CreatedByUserId,
            LastUpdatedByIp = fileUpload.LastUpdatedByIp,
            LastUpdatedByUserId = fileUpload.LastUpdatedByUserId
        };
    }
    
    
    public static FileUploadResult WithValidationErrors(
        this FileUpload fileUpload, IEnumerable<ValidationError> errors)
    {
        fileUpload.ThrowExceptionIfArgumentIsNull(nameof(fileUpload));
        if (errors is null || !errors.Any())
            throw new ArgumentNullException(nameof(errors));

        var result = fileUpload.ToResult();
        return (FileUploadResult)result.WithValidationErrors(errors);
    }
}