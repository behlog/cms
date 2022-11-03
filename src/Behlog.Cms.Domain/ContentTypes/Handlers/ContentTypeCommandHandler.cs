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
    private readonly IBehlogManager _manager;
    private readonly IContentTypeReadStore _readStore;
    private readonly IContentTypeWriteStore _writeStore;

    public ContentTypeCommandHandler(
        IBehlogManager manager, IContentTypeReadStore readStore, IContentTypeWriteStore writeStore)
    {
        _manager = manager ?? throw new ArgumentNullException(nameof(manager));
        _readStore = readStore ?? throw new ArgumentNullException(nameof(readStore));
        _writeStore = writeStore ?? throw new ArgumentNullException(nameof(writeStore));
    }


    public async Task<ContentTypeResult> HandleAsync(
        CreateContentTypeCommand command, CancellationToken cancellationToken = default)
    {
        command.ThrowExceptionIfArgumentIsNull(nameof(command));
        cancellationToken.ThrowIfCancellationRequested();

        var contentType = await ContentType.CreateAsync(command, _manager);
        await _writeStore.AddAsync(contentType, cancellationToken).ConfigureAwait(false);

        return await Task.FromResult(contentType.ToResult());
    }

    public async Task HandleAsync(
        UpdateContentTypeCommand command, CancellationToken cancellationToken = default)
    {
        command.ThrowExceptionIfArgumentIsNull(nameof(command));
        cancellationToken.ThrowIfCancellationRequested();

        var contentType = await _readStore.FindAsync(command.Id, cancellationToken).ConfigureAwait(false);
        await contentType.UpdateAsync(command, _manager);
        _writeStore.MarkForUpdate(contentType);
        await _writeStore.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
    }

    public async Task HandleAsync(
        SoftDeleteContentTypeCommand command, CancellationToken cancellationToken = default)
    {
        command.ThrowExceptionIfArgumentIsNull(nameof(command));
        cancellationToken.ThrowIfCancellationRequested();

        var contentType = await _readStore.FindAsync(command.Id, cancellationToken).ConfigureAwait(false);
        await contentType.SoftDeleteAsync(_manager);
        await _writeStore.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
    }
    
}