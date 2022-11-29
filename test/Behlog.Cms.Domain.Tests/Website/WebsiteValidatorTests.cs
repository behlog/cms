using System;
using System.Diagnostics;
using System.Linq;
using Behlog.Cms.Commands;
using Behlog.Cms.Errors;
using Behlog.Cms.Handlers;
using Behlog.Cms.Validations;
using FluentAssertions;
using Xunit;

namespace Behlog.Cms.Domain.Tests.Website;

public class WebsiteValidatorTests
{
    private readonly CreateWebsiteCommand validCreateCommand;

    public WebsiteValidatorTests()
    {
        validCreateCommand = new CreateWebsiteCommand(
            "Behlog", "Behlog", "Behlog CMS", "Behlog, CMS",
            "https//behlog.ir", Guid.NewGuid().ToString(),
            PersianLanguage.Id, null, false,
            "info@behlog.ir", null);
    }
    
    [Fact]
    public void is_valid()
    {
        var validation = CreateWebsiteCommandValidator.Run(validCreateCommand);

        validation.IsValid.Should().Be(true);
    }

    
    [Fact]
    public void name_cannot_more_than_256_chars()
    {
        var big_name = "behlog";
        for (int i = 1; i < 10; i++)
            big_name += big_name;
        
        Debug.Write(big_name.Length);
        
        var command = new CreateWebsiteCommand(
            big_name, "Behlog", "Behlog CMS", "Behlog, CMS",
            "https//behlog.ir", Guid.NewGuid().ToString(),
            PersianLanguage.Id, null, false,
            "info@behlog.ir", null);

        var validation = CreateWebsiteCommandValidator.Run(command);
        validation.IsValid.Should().Be(false);

        validation.Errors.Should().Contain(
            validation.Errors.First(
                _ => _.FieldName.Equals(nameof(command.Name)) &&
                     _.ErrorCode.Equals(WebsiteErrorCodes.NameMaxLen)
            )
        );
    }

    [Theory]
    [InlineData("hi__behlog.ir")]
    [InlineData("www.hi!behlog.ir")]
    [InlineData(".mm,,,))@uisdi")]
    public void email_cannot_have_incorrect_format(string email)
    {
        var command = new CreateWebsiteCommand(
            "Behlog", "Behlog", "Behlog CMS", "Behlog, CMS",
            "https//behlog.ir", Guid.NewGuid().ToString(),
            PersianLanguage.Id, null, false,
            email, null);
        
        var validation = CreateWebsiteCommandValidator.Run(command);
        validation.IsValid.Should().Be(false);
        
        validation.Errors.Should().Contain(
            validation.Errors.First(
                _ => _.FieldName.Equals(nameof(command.Email)) &&
                     _.ErrorCode.Equals(WebsiteErrorCodes.EmailFormat)
            )
        );
    }


    [Theory]
    [InlineData("", "Behlog", "123113431")]
    [InlineData("Behlog", null, "12233")]
    [InlineData("Behlog", "Behlog", null)]
    [InlineData(null, null, "")]
    [InlineData("Behlog", "", "m")]
    public void is_not_valid_when_one_of_required_fields_are_empty_or_null(
        string name, string title, string ownerUserId)
    {
        var command = new CreateWebsiteCommand(
            name, title, "Behlog CMS", "Behlog, CMS",
            "https//behlog.ir", ownerUserId,
            PersianLanguage.Id, null, false,
            "hi@behlog.ir", null);

        var validation = CreateWebsiteCommandValidator.Run(command);
        validation.IsValid.Should().Be(false);
    }
}