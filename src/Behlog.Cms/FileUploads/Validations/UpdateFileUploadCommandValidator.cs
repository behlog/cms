using Behlog.Cms.Commands;
using Behlog.Cms.Errors;
using Behlog.Cms.FileUploads.Internal;
using Behlog.Core.Contracts;
using Behlog.Core.Models;
using Behlog.Core.Validations;
using Behlog.Extensions;

namespace Behlog.Cms.Validations;


public class UpdateFileUploadCommandValidator :
    IBehlogCommandValidator<UpdateFileUploadCommand, CommandResult>
{
    
    public ValidatorResult Validate(UpdateFileUploadCommand command)
    {
        command.ThrowExceptionIfArgumentIsNull(nameof(command));

        return ValidatorResult.Create()
                .ThrowExceptionIfIdIsNotValid(command.Id)

                .HasMaxLenght(command.Title, 1000, nameof(command.Title),
                    FileUploadErrorCodes.GetMessage(FileUploadErrorCodes.TitleMaxLen)!,
                    FileUploadErrorCodes.TitleMaxLen)

                .HasMaxLenght(command.AltTitle, 1000, nameof(command.AltTitle),
                    FileUploadErrorCodes.GetMessage(FileUploadErrorCodes.AltTitleMaxLen)!,
                    FileUploadErrorCodes.AltTitleMaxLen)

                .HasMaxLenght(command.Description, 2000, nameof(command.Description),
                    FileUploadErrorCodes.GetMessage(FileUploadErrorCodes.DescriptionMaxLen)!,
                    FileUploadErrorCodes.DescriptionMaxLen)
            ;
    }


    public static ValidatorResult Run(UpdateFileUploadCommand command)
    {
        return new UpdateFileUploadCommandValidator().Validate(command);
    }
}