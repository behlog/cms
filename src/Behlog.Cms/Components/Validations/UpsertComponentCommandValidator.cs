using Behlog.Extensions;
using Behlog.Cms.Models;
using Behlog.Core.Models;
using Behlog.Cms.Commands;
using Behlog.Core.Contracts;
using Behlog.Core.Validations;
using static Behlog.Cms.Validations.ComponentErrorCodes;

namespace Behlog.Cms.Validations;


public class UpsertComponentCommandValidator 
    : IBehlogCommandValidator<UpsertComponentCommand, CommandResult<ComponentResult>>
{
    
    public ValidatorResult Validate(UpsertComponentCommand command)
    {
        command.ThrowExceptionIfArgumentIsNull(nameof(command));

        return ValidatorResult.Create()
                .IsRequired(command.Name, nameof(command.Name),
                    GetMessage(NameIsNull)!, NameIsNull)

                .HasMaxLenght(command.Name, 256, nameof(command.Name),
                    GetMessage(NameMaxLen)!, NameMaxLen)

                .IsRequired(command.Title, nameof(command.Title),
                    GetMessage(TitleIsNull)!, TitleIsNull)

                .HasMaxLenght(command.Title, 256, nameof(command.Title),
                    GetMessage(TitleMaxLen)!, TitleMaxLen)

                .IsRequired(command.Category, nameof(command.Category),
                    GetMessage(CategoryIsNull)!, CategoryIsNull)

                .HasMaxLenght(command.Category, 256, nameof(command.Category),
                    GetMessage(CategoryMaxLen)!, CategoryMaxLen)
                
                .IsRequired(command.ComponentType, nameof(command.ComponentType),
                    GetMessage(ComponentTypeIsNull)!, ComponentTypeMaxLen)
            
                .HasMaxLenght(command.Description, 1000, nameof(command.Description),
                    GetMessage(DescriptionMaxLen)!, DescriptionMaxLen)
                
                .HasMaxLenght(command.Author, 256, nameof(command.Author),
                    GetMessage(AuthorMaxLen)!, AuthorMaxLen)
                
                .HasMaxLenght(command.AuthorEmail, 1000, nameof(command.AuthorEmail),
                    GetMessage(AuthorEmailMaxLen)!, AuthorEmailMaxLen)
                
                .IsEmailFormatCorrect(command.AuthorEmail!, nameof(command.AuthorEmail),
                    GetMessage(AuthorEmailFormat)!, AuthorEmailFormat)
            
                .HasMaxLenght(command.Keywords, 256, nameof(command.Keywords),
                    GetMessage(KeywordsMaxLen)!, KeywordsMaxLen)
                
                .HasMaxLenght(command.ViewPath, 2000, nameof(command.ViewPath),
                    GetMessage(ViewPathMaxLen)!, ViewPathMaxLen)
            ;
    }


    public static ValidatorResult Run(UpsertComponentCommand command)
        => new UpsertComponentCommandValidator().Validate(command);

}