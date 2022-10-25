using Behlog.Cms.Commands;
using Behlog.Cms.Domain.Models;
using Behlog.Cms.Repository;
using Behlog.Core;
using Behlog.Extensions;

namespace Behlog.Cms.Domain.Handlers;


public class ContentCommandHandler :
    IBehlogCommandHandler<CreateContentCommand, ContentResult>,
    IBehlogCommandHandler<UpdateContentCommand>
{
    private readonly IBehlogManager _manager;
    private readonly IContentRepository _contentRepository;

    public ContentCommandHandler(
        IBehlogManager manager, IContentRepository contentRepository)
    {
        _manager = manager ?? throw new ArgumentNullException(nameof(manager));
        _contentRepository = contentRepository ?? throw new ArgumentNullException(nameof(contentRepository));
    }

    public async Task<ContentResult> HandleAsync(
        CreateContentCommand command, CancellationToken cancellationToken = default)
    {
        command.ThrowExceptionIfArgumentIsNull(nameof(command));

        var content = await Content.CreateAsync(command, _manager);
        await _contentRepository.AddAsync(content, cancellationToken).ConfigureAwait(false);
        
        //TODO : use mapper exts here.
        return new ContentResult(
            content.Id, content.Title, content.Slug, content.ContentTypeId,
            "", "", content.Body, content.BodyType, content.AuthorUserId, command.Summary,
            content.Status, content.AltTitle, command.OrderNum, content.Categories);
    }

    public async Task HandleAsync(
        UpdateContentCommand command, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}