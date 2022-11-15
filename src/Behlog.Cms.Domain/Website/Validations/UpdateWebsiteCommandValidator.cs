using Behlog.Extensions;
using Behlog.Core.Models;
using Behlog.Cms.Commands;
using Behlog.Core.Contracts;
using Behlog.Core.Validations;

namespace Behlog.Cms.Handlers;


public class UpdateWebsiteCommandValidator :
    IBehlogCommandValidator<UpdateWebsiteCommand, CommandResult>
{
    
    public ValidatorResult Validate(UpdateWebsiteCommand command)
    {
        command.ThrowExceptionIfArgumentIsNull(nameof(command));
        
        return ValidatorResult.Create()
            .HasMaxLenght(command.Name, 256, nameof(command.Name), "Name maxlen is 256")
            .IsNotNullOrEmpty(command.Name, nameof(command.Name), "Name can't be null")
            .IsNotNullOrEmpty(command.Title, nameof(command.Title), "Title can't be null.")
            .HasMaxLenght(command.Description, 2000, nameof(command.Description), "Description Maxlen is 2000")
            .HasMaxLenght(command.Keywords, 1000, nameof(command.Keywords), "Kmeywords MaxLen is 1000")
            .HasMaxLenght(command.Url, 2000, nameof(command.Url), "Url len is 2000")
            .IsEmailFormatCorrect(command.Email, nameof(command.Email), "Email format is incorrect")
            .HasMaxLenght(command.CopyrightText, 1000, nameof(command.CopyrightText), "Maxlen is 1000");
    }

    public static ValidatorResult Run(UpdateWebsiteCommand command)
    {
        return new UpdateWebsiteCommandValidator().Validate(command);
    }
}