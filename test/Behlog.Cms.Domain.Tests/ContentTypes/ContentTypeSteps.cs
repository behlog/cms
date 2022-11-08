using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Behlog.Core;
using NSubstitute;
using Behlog.Core;
using Behlog.Cms.Events;
using Behlog.Cms.Commands;
using Behlog.Cms.Models;

namespace Behlog.Cms.Domain.Tests.ContentTypes;


public class ContentTypeSteps : BaseTestSteps
{
    private readonly Dictionary<string, ContentType> _contentTypes = new();
    private readonly IBehlogManager _manager;
    
    public ContentTypeSteps()
    {
        _manager = Substitute.For<IBehlogManager>();
    }


    public void IRegisterAContentTypeWithCommand(CreateContentTypeCommand command)
    {
        var contentType = ContentType.Create(command);
        _contentTypes[command.Title] = contentType;
    }


    public void ICanFinAContentTypeCreatedWithTitle(string title)
    {
        var contentType = _contentTypes[title];
        var expected = new ContentTypeCreatedEvent(
            contentType.Id, contentType.SystemName, contentType.Title,
            contentType.LangId, contentType.Slug, contentType.Description);
        _contentTypes[title].ShouldContainsExpectedDomainEvent(expected);
    }
}