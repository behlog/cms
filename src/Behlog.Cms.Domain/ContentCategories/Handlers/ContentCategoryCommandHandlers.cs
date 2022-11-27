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
    private readonly IContentCategoryWriteStore _writeStore;
    private readonly IContentCategoryReadStore _readStore;
    private readonly IBehlogApplicationContext _appContext;
    private readonly IIdyfaUserContext _userContext;
    private readonly ISystemDateTime _dateTime;

    public ContentCategoryCommandHandlers(
        IContentCategoryWriteStore writeStore, IContentCategoryReadStore readStore,
        IBehlogApplicationContext appContext, IIdyfaUserContext userContext, ISystemDateTime dateTime)
    {
        _writeStore = writeStore ?? throw new ArgumentNullException(nameof(writeStore));
        _readStore = readStore ?? throw new ArgumentNullException(nameof(readStore));
        _appContext = appContext ?? throw new ArgumentNullException(nameof(appContext));
        _userContext = userContext ?? throw new ArgumentNullException(nameof(userContext));
        _dateTime = dateTime ?? throw new ArgumentNullException(nameof(dateTime));
    }
    
    public async Task<ContentCategoryResult> HandleAsync(
        CreateContentCategoryCommand command, CancellationToken cancellationToken = default)
    {
        command.ThrowExceptionIfArgumentIsNull(nameof(command));

        var category = ContentCategory.Create(command, _userContext, _appContext, _dateTime);
        category.SetIdentityOnAdd(_userContext, _appContext);
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
        category.SetIdentityOnUpdate(_userContext, _appContext);
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
        category.SetIdentityOnUpdate(_userContext, _appContext);
        
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