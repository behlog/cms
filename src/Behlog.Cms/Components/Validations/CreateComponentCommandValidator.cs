using Behlog.Cms.Commands;
using Behlog.Cms.Models;
using Behlog.Core.Contracts;
using Behlog.Core.Models;
using Behlog.Core.Validations;
using Behlog.Extensions;

namespace Behlog.Cms.Components.Validations;

public class CreateComponentCommandValidator :
    IBehlogCommandValidator<CreateComponentCommand, CommandResult<ComponentResult>>
{
    
    public ValidatorResult Validate(CreateComponentCommand command)
    {
        command.ThrowExceptionIfArgumentIsNull(nameof(command));

        return ValidatorResult.Create()
                .IsRequired(command.Name, nameof(command.Name),
                    ComponentErrorCodes.GetMessage(ComponentErrorCodes.NameIsNull)!,
                    ComponentErrorCodes.NameIsNull)

                .HasMaxLenght(command.Name, 256, nameof(command.Name),
                    ComponentErrorCodes.GetMessage(ComponentErrorCodes.NameMaxLen)!,
                    ComponentErrorCodes.NameMaxLen)

                .IsRequired(command.Title, nameof(command.Title),
                    ComponentErrorCodes.GetMessage(ComponentErrorCodes.TitleIsNull)!,
                    ComponentErrorCodes.TitleIsNull)

                .HasMaxLenght(command.Title, 256, nameof(command.Title),
                    ComponentErrorCodes.GetMessage(ComponentErrorCodes.TitleMaxLen)!,
                    ComponentErrorCodes.TitleMaxLen)

                .IsRequired(command.Category, nameof(command.Category),
                    ComponentErrorCodes.GetMessage(ComponentErrorCodes.CategoryIsNull)!,
                    ComponentErrorCodes.CategoryIsNull)

                .HasMaxLenght(command.Category, 256, nameof(command.Category),
                    ComponentErrorCodes.GetMessage(ComponentErrorCodes.CategoryMaxLen)!,
                    ComponentErrorCodes.CategoryMaxLen)
                
                .IsRequired(command.ComponentType, nameof(command.ComponentType),
                    ComponentErrorCodes.GetMessage(ComponentErrorCodes.ComponentTypeIsNull)!,
                    ComponentErrorCodes.ComponentTypeMaxLen)
            
                .HasMaxLenght(command.Description, 1000, nameof(command.Description),
                    ComponentErrorCodes.GetMessage(ComponentErrorCodes.DescriptionMaxLen)!,
                    ComponentErrorCodes.DescriptionMaxLen)
                
                .HasMaxLenght(command.Author, 256, nameof(command.Author),
                    ComponentErrorCodes.GetMessage(ComponentErrorCodes.AuthorMaxLen)!,
                    ComponentErrorCodes.AuthorMaxLen)
                
                .HasMaxLenght(command.AuthorEmail, 1000, nameof(command.AuthorEmail),
                    ComponentErrorCodes.GetMessage(ComponentErrorCodes.AuthorEmailMaxLen)!,
                    ComponentErrorCodes.AuthorEmailMaxLen)
            
                .HasMaxLenght(command.Keywords, 256, nameof(command.Keywords),
                    ComponentErrorCodes.GetMessage(ComponentErrorCodes.KeywordsMaxLen)!,
                    ComponentErrorCodes.KeywordsMaxLen)
                
                .HasMaxLenght(command.ViewPath, 2000, nameof(command.ViewPath),
                    ComponentErrorCodes.GetMessage(ComponentErrorCodes.ViewPathMaxLen)!,
                    ComponentErrorCodes.ViewPathMaxLen)
            ;
    }


    public static ValidatorResult Run(CreateComponentCommand command)
        => new CreateComponentCommandValidator().Validate(command);
}