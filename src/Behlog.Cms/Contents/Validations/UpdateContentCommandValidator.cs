using Behlog.Cms.Commands;
using Behlog.Cms.Errors;
using Behlog.Core.Contracts;
using Behlog.Core.Models;
using Behlog.Core.Validations;
using Behlog.Extensions;

namespace Behlog.Cms.Validations;


public class UpdateContentCommandValidator :
    IBehlogCommandValidator<UpdateContentCommand, CommandResult>
{
    
    public ValidatorResult Validate(UpdateContentCommand command)
    {
        command.ThrowExceptionIfArgumentIsNull(nameof(command));
        
        return ValidatorResult.Create()
                .ThrowExceptionIfIdIsNotValid(command.Id)
                
                .IsRequired(command.Title, nameof(command.Title),
                    ContentErrorCodes.TitleIsNull,
                    ContentErrorCodes.GetMessage(ContentErrorCodes.TitleIsNull)!)

                .HasMaxLenght(command.Title, 1000, nameof(command.Title),
                    ContentErrorCodes.TitleMaxLen,
                    ContentErrorCodes.GetMessage(ContentErrorCodes.TitleMaxLen)!)

                .HasMaxLenght(command.Slug, 1000, nameof(command.Slug),
                    ContentErrorCodes.SlugMaxLen,
                    ContentErrorCodes.GetMessage(ContentErrorCodes.SlugMaxLen)!)

                .HasMaxLenght(command.Summary, 2000, nameof(command.Summary),
                    ContentErrorCodes.GetMessage(ContentErrorCodes.SummaryMaxLen)!,
                    ContentErrorCodes.SummaryMaxLen)
            
                .HasMaxLenght(command.AltTitle, 1000, nameof(command.AltTitle),
                    ContentErrorCodes.GetMessage(ContentErrorCodes.AltTitleMaxLen)!,
                    ContentErrorCodes.AltTitleMaxLen)
            
                .HasMaxLenght(command.Password, 100, nameof(command.Password),
                    ContentErrorCodes.GetMessage(ContentErrorCodes.PasswordMaxLen)!,
                    ContentErrorCodes.PasswordMaxLen)
            
                .HasMaxLenght(command.IconName, 256, nameof(command.IconName),
                    ContentErrorCodes.GetMessage(ContentErrorCodes.IconMaxLen)!,
                    ContentErrorCodes.IconMaxLen)
            
                // .HasMaxLenght(command.ViewPath, 2000, nameof(command.ViewPath),
                //     ContentErrorCodes.GetMessage(ContentErrorCodes.ViewPathMaxLen)!,
                //     ContentErrorCodes.ViewPathMaxLen)

            ;
    }


    public static ValidatorResult Run(UpdateContentCommand command)
        => new UpdateContentCommandValidator().Validate(command);
}