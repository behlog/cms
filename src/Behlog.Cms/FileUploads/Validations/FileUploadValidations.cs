using Behlog.Cms.FileUploads.Internal;
using Behlog.Core.Validations;

namespace Behlog.Cms.Validations;

public static class FileUploadValidations
{

    public static ValidatorResult FileIsRequired(
        this ValidatorResult result, IFormFile file,
        string errorMessage, string errorCode = "")
    {
        if (file.IsNullOrEmpty())
        {
            return result.WithError(ValidationError
                .Create("File", errorCode, errorMessage));
        }

        return result;
    }
    
}