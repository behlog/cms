using Behlog.Cms.Commands;
using Behlog.Cms.Errors;
using Behlog.Cms.FileUploads.Internal;
using Behlog.Cms.Models;
using Behlog.Core.Contracts;
using Behlog.Core.Models;
using Behlog.Core.Validations;
using Behlog.Extensions;

namespace Behlog.Cms.Validations;


public class CreateFileUploadCommandValidator :
    IBehlogCommandValidator<CreateFileUploadCommand, CommandResult<FileUploadResult>>
{
    
    public ValidatorResult Validate(CreateFileUploadCommand command)
    {
        command.ThrowExceptionIfArgumentIsNull(nameof(command));

        return ValidatorResult.Create()
                .FileIsRequired(command.FileData,
                    FileUploadErrorCodes.GetMessage(FileUploadErrorCodes.FileIsNull)!,
                    FileUploadErrorCodes.FileIsNull)
                
                .HasMaxLenght(command.Title, 1000,
                    FileUploadErrorCodes.GetMessage(FileUploadErrorCodes.TitleMaxLen)!,
                    FileUploadErrorCodes.TitleMaxLen)
            
                .HasMaxLenght(command.AltTitle, 1000,
                    FileUploadErrorCodes.GetMessage(FileUploadErrorCodes.AltFilePathMaxLen)!,
                    FileUploadErrorCodes.AltFilePathMaxLen)
            
                .HasMaxLenght(command.Description, 2000,
                    FileUploadErrorCodes.GetMessage(FileUploadErrorCodes.DescriptionMaxLen)!,
                    FileUploadErrorCodes.DescriptionMaxLen)
            ;
    }


    public static ValidatorResult Run(CreateFileUploadCommand command)
    {
        return new CreateFileUploadCommandValidator().Validate(command);
    }
}