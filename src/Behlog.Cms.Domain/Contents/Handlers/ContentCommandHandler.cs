using Behlog.Cms.Commands;
using Behlog.Cms.Domain.Models;
using Behlog.Cms.Repository;
using Behlog.Core;
using Behlog.Extensions;

namespace Behlog.Cms.Domain.Handlers;


public class ContentCommandHandler :
    IBehlogCommandHandler<CreateContentCommand, ContentResult>,
    IBehlogCommandHandler<UpdateContentCommand>,
    IBehlogCommandHandler<SoftDeleteContentCommand>
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
        var content = await _contentRepository.FindAsync(command.Id).ConfigureAwait(false);
        content.ThrowExceptionIfReferenceIsNull(nameof(content));

        await content.UpdateAsync(command, _manager);
        await _contentRepository.UpdateAsync(content, cancellationToken).ConfigureAwait(false);
    }

    public async Task HandleAsync(
        SoftDeleteContentCommand command, CancellationToken cancellationToken = default)
    {
        command.ThrowExceptionIfArgumentIsNull(nameof(command));

        var content = await _contentRepository.FindAsync(command.Id).ConfigureAwait(false);
        await content.SoftDeleteAsync(_manager);

        await _contentRepository.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
    }
    
    
}