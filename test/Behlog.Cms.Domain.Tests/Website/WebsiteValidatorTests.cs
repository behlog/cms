using System;
using System.Diagnostics;
using System.Linq;
using Behlog.Cms.Commands;
using Behlog.Cms.Handlers;
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
            FarsiLanguage.Id, null, false,
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
            FarsiLanguage.Id, null, false,
            "info@behlog.ir", null);

        var validation = CreateWebsiteCommandValidator.Run(command);
        validation.IsValid.Should().Be(false);

        validation.Items.Should().Contain(
            validation.Items.First(
                _ => _.FieldName.Equals(nameof(command.Name)) &&
                     _.Message.Equals("Name maxlen is 256")
            )
        );
    }

    [Theory]
    [InlineData("hi__behlog.ir")]
    [InlineData("www.hi@behlog.ir")]
    [InlineData("mm,,,))@uisdi")]
    public void email_cannot_have_incorrect_format(string email)
    {
        var command = new CreateWebsiteCommand(
            "Behlog", "Behlog", "Behlog CMS", "Behlog, CMS",
            "https//behlog.ir", Guid.NewGuid().ToString(),
            FarsiLanguage.Id, null, false,
            email, null);
        
        var validation = CreateWebsiteCommandValidator.Run(command);
        validation.IsValid.Should().Be(false);
        
        validation.Items.Should().Contain(
            validation.Items.First(
                _ => _.FieldName.Equals(nameof(command.Email)) &&
                     _.Message.Equals("Email format is incorrect")
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
            FarsiLanguage.Id, null, false,
            "hi@behlog.ir", null);

        var validation = CreateWebsiteCommandValidator.Run(command);
        validation.IsValid.Should().Be(false);
    }
}