using Behlog.Cms.Commands;
using Behlog.Cms.Errors;
using Behlog.Cms.Models;
using Behlog.Core.Contracts;
using Behlog.Core.Models;
using Behlog.Core.Validations;
using Behlog.Extensions;

namespace Behlog.Cms.Validations;


public class CreateContentTypeCommandValidator :
    IBehlogCommandValidator<CreateContentTypeCommand, CommandResult<ContentTypeResult>>
{
    
    public ValidatorResult Validate(CreateContentTypeCommand command)
    {
        command.ThrowExceptionIfArgumentIsNull(nameof(command));

        return ValidatorResult.Create()
                .IsRequired(command.SystemName, nameof(command.SystemName),
                    ContentTypeErrorCodes.GetMessage(ContentTypeErrorCodes.SystemNameIsNull)!,
                    ContentTypeErrorCodes.SystemNameIsNull)
                
                .IsRequired(command.Title, nameof(command.Title),
                    ContentTypeErrorCodes.GetMessage(ContentTypeErrorCodes.TitleIsNull)!,
                    ContentTypeErrorCodes.TitleIsNull)
            
                .IsRequired(command.Slug, nameof(command.Slug),
                    ContentTypeErrorCodes.GetMessage(ContentTypeErrorCodes.SlugIsNull)!,
                    ContentTypeErrorCodes.SlugIsNull)
            
                .HasMaxLenght(command.SystemName, 50, nameof(command.SystemName),
                    ContentTypeErrorCodes.GetMessage(ContentTypeErrorCodes.SystemNameMaxLen)!,
                    ContentTypeErrorCodes.SystemNameMaxLen)
            
                .HasMaxLenght(command.Title, 256, nameof(command.Title),
                    ContentTypeErrorCodes.GetMessage(ContentTypeErrorCodes.TitleMaxLen)!,
                    ContentTypeErrorCodes.TitleMaxLen)
            
                .HasMaxLenght(command.Slug, 256, nameof(command.Slug),
                    ContentTypeErrorCodes.GetMessage(ContentTypeErrorCodes.SlugMaxLen)!,
                    ContentTypeErrorCodes.SlugMaxLen)
            
                .HasMaxLenght(command.Description, 2000, nameof(command.Description),
                    ContentTypeErrorCodes.GetMessage(ContentTypeErrorCodes.DescriptionMaxLen)!,
                    ContentTypeErrorCodes.DescriptionMaxLen)
            ;
    }


    public static ValidatorResult Run(CreateContentTypeCommand command)
        => new CreateContentTypeCommandValidator().Validate(command);
}