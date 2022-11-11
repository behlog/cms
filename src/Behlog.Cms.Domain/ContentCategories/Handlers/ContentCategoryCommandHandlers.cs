using Behlog.Core;
using Behlog.Cms.Store;
using Behlog.Extensions;
using Behlog.Cms.Domain;
using Behlog.Cms.Models;
using Behlog.Cms.Commands;
using Behlog.Core.Contracts;
using Idyfa.Core.Contracts;

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
    private readonly IBehlogApplicationContext _applicationContext;
    private readonly IIdyfaUserContext _userContext;

    public ContentCategoryCommandHandlers(
        IBehlogManager manager, 
        IContentCategoryWriteStore writeStore, 
        IContentCategoryReadStore readStore,
        IBehlogApplicationContext applicationContext,
        IIdyfaUserContext userContext)
    {
        _manager = manager ?? throw new ArgumentNullException(nameof(manager));
        _writeStore = writeStore ?? throw new ArgumentNullException(nameof(writeStore));
        _readStore = readStore ?? throw new ArgumentNullException(nameof(readStore));
        _applicationContext = applicationContext ?? throw new ArgumentNullException(nameof(applicationContext));
        _userContext = userContext ?? throw new ArgumentNullException(nameof(userContext));
    }
    
    public async Task<ContentCategoryResult> HandleAsync(
        CreateContentCategoryCommand command, CancellationToken cancellationToken = default)
    {
        command.ThrowExceptionIfArgumentIsNull(nameof(command));

        var category = ContentCategory.Create(command);
        category.SetIdentityOnAdd(_userContext, _applicationContext);
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
        category.SetIdentityOnUpdate(_userContext, _applicationContext);
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
        category.SetIdentityOnUpdate(_userContext, _applicationContext);
        
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