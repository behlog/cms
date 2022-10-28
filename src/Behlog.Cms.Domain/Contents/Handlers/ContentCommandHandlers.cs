using Behlog.Cms.Commands;
using Behlog.Cms.Models;
using Behlog.Cms.Repository;
using Behlog.Core;
using Behlog.Extensions;

namespace Behlog.Cms.Domain.Handlers;


public class ContentCommandHandlers :
    IBehlogCommandHandler<CreateContentCommand, ContentResult>,
    IBehlogCommandHandler<UpdateContentCommand>,
    IBehlogCommandHandler<SoftDeleteContentCommand>,
    IBehlogCommandHandler<PublishContentCommand, BehlogResult>,
    IBehlogCommandHandler<RemoveContentCommand>
{
    private readonly IBehlogManager _manager;
    private readonly IContentRepository _contentRepository;

    public ContentCommandHandlers(
        IBehlogManager manager, IContentRepository contentRepository)
    {
        _manager = manager ?? throw new ArgumentNullException(nameof(manager));
        _contentRepository = contentRepository 
            ?? throw new ArgumentNullException(nameof(contentRepository));
    }

    public async Task<ContentResult> HandleAsync(
        CreateContentCommand command, CancellationToken cancellationToken = default)
    {
        command.ThrowExceptionIfArgumentIsNull(nameof(command));

        var content = await Content.CreateAsync(command, _manager);
        await _contentRepository.AddAsync(content, cancellationToken).ConfigureAwait(false);

        return await Task.FromResult(content.ToResult());
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
        content.ThrowExceptionIfReferenceIsNull(nameof(content));
        await content.SoftDeleteAsync(_manager);

        await _contentRepository.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
    }
    
    public async Task<BehlogResult> HandleAsync(
        PublishContentCommand command, CancellationToken cancellationToken = default)
    {
        command.ThrowExceptionIfArgumentIsNull(nameof(command));

        var content = await _contentRepository.FindAsync(command.Id);
        content.ThrowExceptionIfReferenceIsNull(nameof(content));
        await content.PublishContentAsync(_manager);

        await _contentRepository.SaveChangesAsync(cancellationToken);

        return BehlogResult.Create();
    }

    public async Task HandleAsync(
        RemoveContentCommand command, CancellationToken cancellationToken = default)
    {
        command.ThrowExceptionIfArgumentIsNull(nameof(command));

        var content = await _contentRepository.FindAsync(command.Id).ConfigureAwait(false);
        content.ThrowExceptionIfReferenceIsNull(nameof(content));
        await _contentRepository.DeleteAsync(content, cancellationToken).ConfigureAwait(false);
    }
}