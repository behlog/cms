using Behlog.Extensions;
using Behlog.Core.Models;
using Behlog.Cms.Commands;
using Behlog.Core.Contracts;
using Behlog.Core.Validations;
using static Behlog.Cms.Validations.CommentErrorCodes;

namespace Behlog.Cms.Validations;


public class UpdateCommentCommandValidator 
    : IBehlogCommandValidator<UpdateCommentCommand, CommandResult>
{
    
    public ValidatorResult Validate(UpdateCommentCommand command)
    {
        command.ThrowExceptionIfArgumentIsNull(nameof(command));

        return ValidatorResult.Create()
                .ThrowExceptionIfIdIsNotValid(command.Id)

                .IsRequired(command.Body, nameof(command.Body),
                    BodyIsNull, GetMessage(BodyIsNull)!)

                .HasMaxLenght(command.Title, 256, nameof(command.Title),
                    TitleMaxLen, GetMessage(TitleMaxLen)!)

                .HasMaxLenght(command.Body, 4000, nameof(command.Body),
                    BodyMaxLen, GetMessage(BodyMaxLen)!)

                .HasMaxLenght(command.Email, 1000, nameof(command.Email),
                    EmailMaxLen, GetMessage(EmailMaxLen)!)

                .HasMaxLenght(command.WebUrl, 2000, nameof(command.WebUrl),
                    WebUrlMaxLen, GetMessage(WebUrlMaxLen)!)
            ;
    }
    
    
    public ValidatorResult Run(UpdateCommentCommand command)
    {
        return new UpdateCommentCommandValidator().Validate(command);
    }
}