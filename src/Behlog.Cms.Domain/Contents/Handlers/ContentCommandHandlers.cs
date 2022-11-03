using Behlog.Cms.Commands;
using Behlog.Cms.Models;
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
    private readonly IContentReadStore _readStore;
    private readonly IContentWriteStore _writeStore;

    public ContentCommandHandlers(
        IBehlogManager manager, IContentReadStore readStore, IContentWriteStore writeStore)
    {
        _manager = manager ?? throw new ArgumentNullException(nameof(manager));
        _readStore = readStore;
        _writeStore = writeStore;
    }

    public async Task<ContentResult> HandleAsync(
        CreateContentCommand command, CancellationToken cancellationToken = default)
    {
        command.ThrowExceptionIfArgumentIsNull(nameof(command));

        var content = await Content.CreateAsync(command, _manager);
        await _writeStore.AddAsync(content, cancellationToken).ConfigureAwait(false);

        return await Task.FromResult(content.ToResult());
    }

    public async Task HandleAsync(
        UpdateContentCommand command, CancellationToken cancellationToken = default)
    {
        var content = await _readStore.FindAsync(command.Id, cancellationToken).ConfigureAwait(false);
        content.ThrowExceptionIfReferenceIsNull(nameof(content));

        await content.UpdateAsync(command, _manager);
        await _writeStore.UpdateAsync(content, cancellationToken).ConfigureAwait(false);
    }

    public async Task HandleAsync(
        SoftDeleteContentCommand command, CancellationToken cancellationToken = default)
    {
        command.ThrowExceptionIfArgumentIsNull(nameof(command));

        var content = await _readStore.FindAsync(command.Id, cancellationToken).ConfigureAwait(false);
        content.ThrowExceptionIfReferenceIsNull(nameof(content));
        await content.SoftDeleteAsync(_manager);

        await _writeStore.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
    }
    
    public async Task<BehlogResult> HandleAsync(
        PublishContentCommand command, CancellationToken cancellationToken = default)
    {
        command.ThrowExceptionIfArgumentIsNull(nameof(command));

        var content = await _readStore.FindAsync(command.Id, cancellationToken).ConfigureAwait(false);
        content.ThrowExceptionIfReferenceIsNull(nameof(content));
        await content.PublishContentAsync(_manager);

        await _writeStore.SaveChangesAsync(cancellationToken);

        return BehlogResult.Create();
    }

    public async Task HandleAsync(
        RemoveContentCommand command, CancellationToken cancellationToken = default)
    {
        command.ThrowExceptionIfArgumentIsNull(nameof(command));

        var content = await _readStore.FindAsync(command.Id, cancellationToken).ConfigureAwait(false);
        content.ThrowExceptionIfReferenceIsNull(nameof(content));
        
        _writeStore.MarkForDelete(content);
        await _writeStore.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
    }
}