using Behlog.Cms.Commands;
using TestStack.BDDfy;
using Xunit;

namespace Behlog.Cms.Domain.Tests.ContentTypes;

public class When_soft_deleting_content_type : ContentTypeSteps
{

    [Fact]
    public void Content_type_soft_deleted()
    {
        var command = new CreateContentTypeCommand(
            "Page", "Page", EnglishLangId, "page", "");

        this.Given(_ => ThereIsARegisteredContentTypeWithCommand(command))
            .When(_ => ISoftDeletedTheContentTypeWithTitle(command.Title))
            .Then(_ => ICanSeeThatContentTypeSoftDeletedWithTitle(command.Title))
            .BDDfy();
    }
}