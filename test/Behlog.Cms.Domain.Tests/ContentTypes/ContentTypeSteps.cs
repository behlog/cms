using System.Collections.Generic;
using System.Threading.Tasks;
using Behlog.Core;
using NSubstitute;
using Behlog.Core;
using Behlog.Cms.Events;
using Behlog.Cms.Commands;
using Behlog.Cms.Models;

namespace Behlog.Cms.Domain.Tests.ContentTypes;


public class ContentTypeSteps
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


    public void ICanFinAContentTypeCreatedWithTitle(string title, ContentTypeResult result)
    {
        var expected = new ContentTypeCreatedEvent(
            result.Id, result.SystemName, result.Title,
            result.LangId, result.Slug, result.Description);
        
    }
}