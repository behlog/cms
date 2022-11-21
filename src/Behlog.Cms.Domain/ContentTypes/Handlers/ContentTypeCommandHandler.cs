using Behlog.Core;
using Behlog.Cms.Store;
using Behlog.Extensions;
using Behlog.Cms.Models;
using Behlog.Cms.Commands;

namespace Behlog.Cms.Handlers;


public class ContentTypeCommandHandler :
    IBehlogCommandHandler<CreateContentTypeCommand, ContentTypeResult>,
    IBehlogCommandHandler<UpdateContentTypeCommand>,
    IBehlogCommandHandler<SoftDeleteContentTypeCommand>
{
    private readonly IBehlogMediator _mediator;
    private readonly IContentTypeReadStore _readStore;
    private readonly IContentTypeWriteStore _writeStore;

    public ContentTypeCommandHandler(
        IBehlogMediator mediator, IContentTypeReadStore readStore, IContentTypeWriteStore writeStore)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        _readStore = readStore ?? throw new ArgumentNullException(nameof(readStore));
        _writeStore = writeStore ?? throw new ArgumentNullException(nameof(writeStore));
    }


    public async Task<ContentTypeResult> HandleAsync(
        CreateContentTypeCommand command, CancellationToken cancellationToken = default)
    {
        command.ThrowExceptionIfArgumentIsNull(nameof(command));
        cancellationToken.ThrowIfCancellationRequested();

        var contentType = ContentType.Create(command);
        await _writeStore.AddAsync(contentType, cancellationToken).ConfigureAwait(false);

        return await Task.FromResult(contentType.ToResult());
    }

    public async Task HandleAsync(
        UpdateContentTypeCommand command, CancellationToken cancellationToken = default)
    {
        command.ThrowExceptionIfArgumentIsNull(nameof(command));
        cancellationToken.ThrowIfCancellationRequested();

        var contentType = await _readStore.FindAsync(command.Id, cancellationToken).ConfigureAwait(false);
        contentType.Update(command);
        _writeStore.MarkForUpdate(contentType);
        await _writeStore.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
    }

    public async Task HandleAsync(
        SoftDeleteContentTypeCommand command, CancellationToken cancellationToken = default)
    {
        command.ThrowExceptionIfArgumentIsNull(nameof(command));
        cancellationToken.ThrowIfCancellationRequested();

        var contentType = await _readStore.FindAsync(command.Id, cancellationToken).ConfigureAwait(false);
        contentType.SoftDelete();
        _writeStore.MarkForUpdate(contentType);
        await _writeStore.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
    }
    
}