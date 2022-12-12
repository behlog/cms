using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Behlog.Core;
using NSubstitute;
using Behlog.Core;
using Behlog.Cms.Events;
using Behlog.Cms.Commands;
using Behlog.Cms.Contracts;
using Behlog.Cms.Models;
using Behlog.Core.Contracts;

namespace Behlog.Cms.Domain.Tests.ContentTypes;


public class ContentTypeSteps : BaseTestSteps
{
    private readonly Dictionary<string, ContentType> _contentTypes = new();
    private readonly IBehlogMediator _mediator;
    private readonly ISystemDateTime _dateTime;
    private readonly IContentTypeService _service;
    
    public ContentTypeSteps()
    {
        _mediator = Substitute.For<IBehlogMediator>();
        _dateTime = Substitute.For<ISystemDateTime>();
        _service = Substitute.For<IContentTypeService>();
    }


    public void IRegisterAContentTypeWithCommand(CreateContentTypeCommand command)
    {
        var contentType = ContentType.CreateAsync(command, _dateTime, _service);
        _contentTypes[command.Title] = contentType.Result;
    }
    
    public void ThereIsARegisteredContentTypeWithCommand(CreateContentTypeCommand command)
    {
        var contentType = ContentType.CreateAsync(command, _dateTime, _service).Result;
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

    public void ISoftDeletedTheContentTypeWithTitle(string title)
    {
        _contentTypes[title].SoftDelete();
    }

    public void ICanSeeThatContentTypeSoftDeletedWithTitle(string title)
    {
        var contentType = _contentTypes[title];
        var expected = new ContentTypeSoftDeletedEvent(contentType.Id);
        _contentTypes[title].ShouldContainsExpectedDomainEvent(expected);
    }
}