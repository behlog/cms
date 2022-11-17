using Behlog.Cms.Commands;
using Behlog.Cms.Domain;
using Behlog.Cms.Models;
using Behlog.Cms.Resources;
using Behlog.Core.Contracts;
using Behlog.Core.Models;
using Behlog.Core.Validations;
using Behlog.Extensions;

namespace Behlog.Cms.Handlers;

public class CreateWebsiteCommandValidator 
    : IBehlogCommandValidator<CreateWebsiteCommand, CommandResult<WebsiteResult>>
{
    
    public ValidatorResult Validate(CreateWebsiteCommand command)
    {
        command.ThrowExceptionIfArgumentIsNull(nameof(command));
        
        return ValidatorResult.Create()
            .HasMaxLenght(command.Name, 256, nameof(command.Name), 
                "Name maxlen is 256", WebsiteErrorCodes.NameMaxLen)
            .IsNotNullOrEmpty(command.Name, nameof(command.Name), 
                "Name can't be null", WebsiteErrorCodes.NameIsNull)
            .IsNotNullOrEmpty(command.Title, nameof(command.Title), 
                "Title can't be null.", WebsiteErrorCodes.TitleIsNull)
            .HasMaxLenght(command.Description, 2000, nameof(command.Description),
                "Description Maxlen is 2000", WebsiteErrorCodes.DescriptionMaxLen)
            .HasMaxLenght(command.Keywords, 1000, nameof(command.Keywords), 
                "Kmeywords MaxLen is 1000", WebsiteErrorCodes.KeywordsMaxLen)
            .HasMaxLenght(command.Url, 2000, nameof(command.Url), 
                "Url len is 2000", WebsiteErrorCodes.UrlMaxLen)
            .IsNotNullOrEmpty(command.OwnerUserId, nameof(command.OwnerUserId), 
                "OwnerUserId null", WebsiteErrorCodes.OwnerUserNull)
            .CheckWebsiteStatusOnCreate(WebsiteStatus.UnderConstruction, 
                "", WebsiteErrorCodes.InvalidStatus)
            .IsEmailFormatCorrect(command.Email, nameof(command.Email), 
                "Email format is incorrect", WebsiteErrorCodes.EmailFormat)
            .HasMaxLenght(command.CopyrightText, 1000, nameof(command.CopyrightText), 
                ValidationMessages.website_cpr_maxlen, WebsiteErrorCodes.CopyrightMaxLen);
    }


    public static ValidatorResult Run(CreateWebsiteCommand command)
    {
        return new CreateWebsiteCommandValidator().Validate(command);
    }
}