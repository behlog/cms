using Behlog.Cms.Commands;
using Behlog.Cms.Domain;
using Behlog.Cms.Models;
using Behlog.Cms.Store;
using Behlog.Core;
using Behlog.Core.Contracts;
using Behlog.Core.Models;
using Behlog.Extensions;
using Idyfa.Core.Contracts;

namespace Behlog.Cms.Handlers;


public class WebsiteCommandHandlers :
    IBehlogCommandHandler<CreateWebsiteCommand, CommandResult<WebsiteResult>>,
    IBehlogCommandHandler<UpdateWebsiteCommand>,
    IBehlogCommandHandler<SoftDeleteWebsiteCommand>,
    IBehlogCommandHandler<RemoveWebsiteCommand>,
    IBehlogCommandHandler<SetWebsiteStatusCommand>

{
    private readonly IIdyfaUserContext _userContext;
    private readonly IBehlogApplicationContext _applicationContext;
    private readonly IWebsiteReadStore _readStore;
    private readonly IWebsiteWriteStore _writeStore;


    public WebsiteCommandHandlers(
        IIdyfaUserContext userContext, IBehlogApplicationContext applicationContext,
        IWebsiteReadStore readStore, IWebsiteWriteStore writeStore)
    {
        _userContext = userContext ?? throw new ArgumentNullException(nameof(userContext));
        _applicationContext = applicationContext ?? throw new ArgumentNullException(nameof(applicationContext));
        _readStore = readStore ?? throw new ArgumentNullException(nameof(readStore));
        _writeStore = writeStore ?? throw new ArgumentNullException(nameof(writeStore));
    }


    public async Task<CommandResult<WebsiteResult>> HandleAsync(
        CreateWebsiteCommand command, CancellationToken cancellationToken = default)
    {
        command.ThrowExceptionIfArgumentIsNull(nameof(command));
        var validation = CreateWebsiteCommandValidator.Run(command);
        if (validation.HasError)
        {
            return CommandResult<WebsiteResult>.WithValidations(validation.Items);
        }

        var website = Website.Create(command);
        _writeStore.MarkForAdd(website);
        await _writeStore.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

        return await Task.FromResult(
            CommandResult<WebsiteResult>.With(website.ToResult())
            );
    }


    public async Task HandleAsync(
        UpdateWebsiteCommand command, CancellationToken cancellationToken = default)
    {
        command.ThrowExceptionIfArgumentIsNull(nameof(command));

        var website = await GetByIdAsync(command.Id, cancellationToken);
        website.ThrowExceptionIfReferenceIsNull(nameof(website));
        website.Update(command, _userContext, _applicationContext);

        await _writeStore.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
    }

    public async Task HandleAsync(
        SoftDeleteWebsiteCommand command, CancellationToken cancellationToken = default)
    {
        command.ThrowExceptionIfArgumentIsNull(nameof(command));

        var website = await GetByIdAsync(command.Id, cancellationToken);
        website.ThrowExceptionIfReferenceIsNull(nameof(website));
        
        website.SoftDelete(_userContext, _applicationContext);
        _writeStore.MarkForUpdate(website);

        await _writeStore.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
    }

    public async Task HandleAsync(
        RemoveWebsiteCommand command, CancellationToken cancellationToken = default)
    {
        command.ThrowExceptionIfArgumentIsNull(nameof(command));

        var website = await GetByIdAsync(command.Id, cancellationToken);
        website.Remove();
        _writeStore.MarkForDelete(website);
        await _writeStore.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
    }

    public async Task HandleAsync(
        SetWebsiteStatusCommand command, CancellationToken cancellationToken = default)
    {
        command.ThrowExceptionIfArgumentIsNull(nameof(command));

        var website = await GetByIdAsync(command.Id, cancellationToken);
        website.ThrowExceptionIfReferenceIsNull(nameof(website));
        
        website.SetStatus(command.Status, _userContext, _applicationContext);
        await _writeStore.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
    }


    #region private helpers

    private async Task<Website> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _readStore.FindAsync(id, cancellationToken).ConfigureAwait(false);
    }

    #endregion
}