namespace Behlog.Cms.Validations;

public class CreateTagCommandValidator :
    IBehlogCommandValidator<CreateTagCommand, CommandResult<TagResult>>
{
    
    public ValidatorResult Validate(CreateTagCommand command)
    {
        command.ThrowExceptionIfArgumentIsNull(nameof(command));

        return ValidatorResult.Create()
            .IsRequired(command.Title, nameof(command.Title),
                TagErrorCodes.GetMessage(TagErrorCodes.TitleIsNull)!,
                TagErrorCodes.TitleIsNull)

            .HasMaxLenght(command.Title, 1000, nameof(command.Title),
                TagErrorCodes.GetMessage(TagErrorCodes.TitleMaxLen)!,
                TagErrorCodes.TitleMaxLen)
            ;
    }


    public static ValidatorResult Run(CreateTagCommand command)
    {
        return new CreateTagCommandValidator().Validate(command);
    }
}