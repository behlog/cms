using System;
using System.Collections.Generic;
using Behlog.Cms.Commands;
using Behlog.Cms.Domain;
using Behlog.Cms.Domain.Models;
using Behlog.Core;
using FizzWare.NBuilder;
using NSubstitute;
using TestStack.BDDfy;
using Xunit;

namespace Behlog.Cms.Domain.Tests.ContentTypes;

public class When_creating_content_type : ContentTypeSteps
{

    public static readonly IEnumerable<object[]> _creatingData =
        new List<object[]>
        {
            new object[]
            {
                "page", "Pages", "page", EnglishLangId, ""
            },
            new object[]
            {
                "Blog", "Weblog", "blog", EnglishLangId, "English blog"
            },
            new object[]
            {
                "Page", "برگه", "page", FarsiLangId, "برگه های وب سایت"
            }
        };

    [Theory]
    [MemberData(nameof(_creatingData))]
    public void Content_type_created_with_valid_properties(
        string systemName, string title, string slug, Guid langId, string description)
    {
        var createContentTypeCmd = new CreateContentTypeCommand(
            systemName, title, langId, slug, description);

        this.Given(_ => IRegisterAContentTypeWithCommand(createContentTypeCmd))
            .Then(_ => ICanFinAContentTypeCreatedWithTitle(title))
            .BDDfy();
    }
}