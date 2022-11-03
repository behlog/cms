using Behlog.Core;
using Behlog.Cms.Store;
using Behlog.Extensions;
using Behlog.Cms.Domain;
using Behlog.Cms.Models;
using Behlog.Cms.Commands;

namespace Behlog.Cms.Handlers;

public class ContentCategoryCommandHandlers :
    IBehlogCommandHandler<CreateContentCategoryCommand, ContentCategoryResult>,
    IBehlogCommandHandler<UpdateContentCategoryCommand>,
    IBehlogCommandHandler<SoftDeleteContentCategoryCommand>,
    IBehlogCommandHandler<RemoveContentCategoryCommand>
{
    private readonly IBehlogManager _manager;
    private readonly IContentCategoryWriteStore _writeStore;
    private readonly IContentCategoryReadStore _readStore;

    public ContentCategoryCommandHandlers(
        IBehlogManager manager, 
        IContentCategoryWriteStore writeStore, 
        IContentCategoryReadStore readStore)
    {
        _manager = manager ?? throw new ArgumentNullException(nameof(manager));
        _writeStore = writeStore ?? throw new ArgumentNullException(nameof(writeStore));
        _readStore = readStore ?? throw new ArgumentNullException(nameof(readStore));
    }
    
    public async Task<ContentCategoryResult> HandleAsync(
        CreateContentCategoryCommand command, CancellationToken cancellationToken = default)
    {
        command.ThrowExceptionIfArgumentIsNull(nameof(command));

        var category = ContentCategory.Create(command);
        await _writeStore.AddAsync(category, cancellationToken).ConfigureAwait(false);

        return await Task.FromResult(category.ToResult());
    }

    public async Task HandleAsync(
        UpdateContentCategoryCommand command, CancellationToken cancellationToken = default)
    {
        command.ThrowExceptionIfArgumentIsNull(nameof(command));

        var category = await _readStore.FindAsync(command.Id, cancellationToken).ConfigureAwait(false);
        category.ThrowExceptionIfReferenceIsNull(nameof(category));
        
        category.Update(command);
        _writeStore.MarkForUpdate(category);
        await _writeStore.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
    }

    public async Task HandleAsync(
        SoftDeleteContentCategoryCommand command,
        CancellationToken cancellationToken = default)
    {
        command.ThrowExceptionIfArgumentIsNull(nameof(command));

        var category = await _readStore.FindAsync(command.Id, cancellationToken).ConfigureAwait(false);
        category.ThrowExceptionIfReferenceIsNull(nameof(category));
        
        category.SoftDelete();
        _writeStore.MarkForUpdate(category);
        await _writeStore.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
    }


    public async Task HandleAsync(
        RemoveContentCategoryCommand command, 
        CancellationToken cancellationToken = new CancellationToken())
    {
        command.ThrowExceptionIfArgumentIsNull(nameof(command));

        var category = await _readStore.FindAsync(command.Id, cancellationToken).ConfigureAwait(false);
        category.ThrowExceptionIfReferenceIsNull(nameof(category));

        await _writeStore.DeleteAsync(category, cancellationToken).ConfigureAwait(false);
    }
}