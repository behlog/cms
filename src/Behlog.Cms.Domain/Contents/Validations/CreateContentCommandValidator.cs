using Behlog.Cms.Models;
using Behlog.Extensions;
using Behlog.Core.Models;
using Behlog.Cms.Commands;
using Behlog.Core.Contracts;
using Behlog.Core.Validations;

namespace Behlog.Cms.Validations;


public class CreateContentCommandValidator :
    IBehlogCommandValidator<CreateContentCommand, CommandResult<ContentResult>>
{
    
    public ValidatorResult Validate(CreateContentCommand command)
    {
        command.ThrowExceptionIfArgumentIsNull(nameof(command));

        return ValidatorResult.Create()
            .IsRequired(command.Title, nameof(command.Title),
                ContentErrorCodes.TitleIsNull,
                ContentErrorCodes.GetMessage(ContentErrorCodes.TitleIsNull)!)
            
            .HasMaxLenght(command.Title, 1000, nameof(command.Title),
                ContentErrorCodes.TitleMaxLen,
                ContentErrorCodes.GetMessage(ContentErrorCodes.TitleMaxLen)!)
            
            .HasMaxLenght(command.Slug, 1000, nameof(command.Slug),
                ContentErrorCodes.SlugMaxLen,
                ContentErrorCodes.GetMessage(ContentErrorCodes.SlugMaxLen)!);

    }


    public static ValidatorResult Run(CreateContentCommand command)
    {
        return new CreateContentCommandValidator().Validate(command);
    }
}