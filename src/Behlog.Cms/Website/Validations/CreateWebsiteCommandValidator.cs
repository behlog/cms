using Behlog.Cms.Domain;
using Behlog.Cms.Models;
using Behlog.Extensions;
using Behlog.Core.Models;
using Behlog.Cms.Commands;
using Behlog.Core.Contracts;
using Behlog.Core.Validations;
using Behlog.Cms.Errors;


namespace Behlog.Cms.Validations;


public class CreateWebsiteCommandValidator 
    : IBehlogCommandValidator<CreateWebsiteCommand, CommandResult<WebsiteResult>>
{
    
    public ValidatorResult Validate(CreateWebsiteCommand command)
    {
        command.ThrowExceptionIfArgumentIsNull(nameof(command));
        
        return ValidatorResult.Create()
            .HasMaxLenght(command.Name, 256, nameof(command.Name), 
                WebsiteErrorCodes.GetMessage(WebsiteErrorCodes.NameMaxLen)!, 
                WebsiteErrorCodes.NameMaxLen)
            
            .IsRequired(command.Name, nameof(command.Name), 
                WebsiteErrorCodes.GetMessage(WebsiteErrorCodes.NameIsNull)!, 
                WebsiteErrorCodes.NameIsNull)
            
            .IsRequired(command.Title, nameof(command.Title), 
                WebsiteErrorCodes.GetMessage(WebsiteErrorCodes.TitleIsNull)!, 
                WebsiteErrorCodes.TitleIsNull)
            
            .HasMaxLenght(command.Title, 256, nameof(command.Title),
                WebsiteErrorCodes.GetMessage(WebsiteErrorCodes.TitleMaxLen)!,
                WebsiteErrorCodes.TitleMaxLen)
            
            .HasMaxLenght(command.Description, 2000, nameof(command.Description),
                WebsiteErrorCodes.GetMessage(WebsiteErrorCodes.DescriptionMaxLen)!,
                WebsiteErrorCodes.DescriptionMaxLen)
            
            .HasMaxLenght(command.Keywords, 1000, nameof(command.Keywords), 
                WebsiteErrorCodes.GetMessage(WebsiteErrorCodes.KeywordsMaxLen)!, 
                WebsiteErrorCodes.KeywordsMaxLen)
            
            .HasMaxLenght(command.Url, 2000, nameof(command.Url), 
                WebsiteErrorCodes.GetMessage(WebsiteErrorCodes.UrlMaxLen)!, 
                WebsiteErrorCodes.UrlMaxLen)
            
            .IsRequired(command.OwnerUserId, nameof(command.OwnerUserId), 
                WebsiteErrorCodes.GetMessage(WebsiteErrorCodes.OwnerUserNull)!, 
                WebsiteErrorCodes.OwnerUserNull)
            
            .CheckWebsiteStatusOnCreate(WebsiteStatusEnum.UnderConstruction,
                WebsiteErrorCodes.GetMessage(WebsiteErrorCodes.InvalidStatus)!,
                WebsiteErrorCodes.InvalidStatus)
            
            .IsEmailFormatCorrect(command.Email, nameof(command.Email), 
                WebsiteErrorCodes.GetMessage(WebsiteErrorCodes.EmailFormat)!, 
                WebsiteErrorCodes.EmailFormat)
            
            .HasMaxLenght(command.CopyrightText, 1000, nameof(command.CopyrightText), 
                WebsiteErrorCodes.GetMessage(WebsiteErrorCodes.CopyrightMaxLen)!, 
                WebsiteErrorCodes.CopyrightMaxLen);
    }


    public static ValidatorResult Run(CreateWebsiteCommand command)
    {
        return new CreateWebsiteCommandValidator().Validate(command);
    }
}