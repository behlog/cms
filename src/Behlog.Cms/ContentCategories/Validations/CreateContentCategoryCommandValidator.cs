using Behlog.Cms.Commands;
using Behlog.Cms.Models;
using Behlog.Core.Contracts;
using Behlog.Core.Models;
using Behlog.Core.Validations;
using Behlog.Extensions;

namespace Behlog.Cms.Validations;


public class CreateContentCategoryCommandValidator :
    IBehlogCommandValidator<CreateContentCategoryCommand, CommandResult<ContentCategoryResult>>
{

    public ValidatorResult Validate(CreateContentCategoryCommand command)
    {
        command.ThrowExceptionIfArgumentIsNull(nameof(command));

        return ValidatorResult.Create()
                .IsRequired(command.Title, nameof(command.Title),
                    ContentCategoryErrorCodes.GetMessage(ContentCategoryErrorCodes.TitleIsNull)!,
                    ContentCategoryErrorCodes.TitleIsNull)

                .HasMaxLenght(command.Title, 256, nameof(command.Title),
                    ContentCategoryErrorCodes.GetMessage(ContentCategoryErrorCodes.TitleMaxLen)!,
                    ContentCategoryErrorCodes.TitleMaxLen)

                .HasMaxLenght(command.AltTitle, 500, nameof(command.AltTitle),
                    ContentCategoryErrorCodes.GetMessage(ContentCategoryErrorCodes.AltTitleMaxLen)!,
                    ContentCategoryErrorCodes.AltTitleMaxLen)

                .IsRequired(command.Slug, nameof(command.Slug),
                    ContentCategoryErrorCodes.GetMessage(ContentCategoryErrorCodes.SlugIsNull)!,
                    ContentCategoryErrorCodes.SlugIsNull)

                .HasMaxLenght(command.Slug, 256, nameof(command.Slug),
                    ContentCategoryErrorCodes.GetMessage(ContentCategoryErrorCodes.SlugMaxLen)!,
                    ContentCategoryErrorCodes.SlugMaxLen)

                .HasMaxLenght(command.Description, 2000, nameof(command.Description),
                    ContentCategoryErrorCodes.GetMessage(ContentCategoryErrorCodes.DescriptionMaxLen)!,
                    ContentCategoryErrorCodes.DescriptionMaxLen)
            ;
    }


    public static ValidatorResult Run(CreateContentCategoryCommand command)
    {
        return new CreateContentCategoryCommandValidator().Validate(command);
    }

}