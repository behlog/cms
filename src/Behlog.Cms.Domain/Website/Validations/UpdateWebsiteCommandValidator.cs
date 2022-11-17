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
            .HasMaxLenght(command.Name, 256, nameof(command.Name), 
                WebsiteErrorCodes.GetMessage(WebsiteErrorCodes.NameMaxLen)!, 
                WebsiteErrorCodes.NameMaxLen)
            
            .IsNotNullOrEmpty(command.Name, nameof(command.Name), 
                WebsiteErrorCodes.GetMessage(WebsiteErrorCodes.NameIsNull)!, 
                WebsiteErrorCodes.NameIsNull)
            
            .IsNotNullOrEmpty(command.Title, nameof(command.Title), 
                WebsiteErrorCodes.GetMessage(WebsiteErrorCodes.TitleIsNull)!, 
                WebsiteErrorCodes.TitleIsNull)
            
            .HasMaxLenght(command.Description, 2000, nameof(command.Description),
                WebsiteErrorCodes.GetMessage(WebsiteErrorCodes.DescriptionMaxLen)!,
                WebsiteErrorCodes.DescriptionMaxLen)
            
            .HasMaxLenght(command.Keywords, 1000, nameof(command.Keywords), 
                WebsiteErrorCodes.GetMessage(WebsiteErrorCodes.KeywordsMaxLen)!, 
                WebsiteErrorCodes.KeywordsMaxLen)
            
            .HasMaxLenght(command.Url, 2000, nameof(command.Url), 
                WebsiteErrorCodes.GetMessage(WebsiteErrorCodes.UrlMaxLen)!, 
                WebsiteErrorCodes.UrlMaxLen)

            .IsEmailFormatCorrect(command.Email, nameof(command.Email), 
                WebsiteErrorCodes.GetMessage(WebsiteErrorCodes.EmailFormat)!, 
                WebsiteErrorCodes.EmailFormat)
            
            .HasMaxLenght(command.CopyrightText, 1000, nameof(command.CopyrightText), 
                WebsiteErrorCodes.GetMessage(WebsiteErrorCodes.CopyrightMaxLen)!, 
                WebsiteErrorCodes.CopyrightMaxLen);
    }

    public static ValidatorResult Run(UpdateWebsiteCommand command)
    {
        return new UpdateWebsiteCommandValidator().Validate(command);
    }
}