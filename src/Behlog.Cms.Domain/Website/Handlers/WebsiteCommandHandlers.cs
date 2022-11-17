using Behlog.Cms.Commands;
using Behlog.Cms.Domain;
using Behlog.Cms.Models;
using Behlog.Cms.Store;
using Behlog.Core;
using Behlog.Core.Contracts;
using Behlog.Core.Domain;
using Behlog.Core.Models;
using Behlog.Core.Validations;
using Behlog.Extensions;
using Idyfa.Core.Contracts;
using Microsoft.Extensions.Logging;

namespace Behlog.Cms.Handlers;


public class WebsiteCommandHandlers : BehlogBaseCommandHandler,
    IBehlogCommandHandler<CreateWebsiteCommand, CommandResult<WebsiteResult>>,
    IBehlogCommandHandler<UpdateWebsiteCommand, CommandResult>,
    IBehlogCommandHandler<SoftDeleteWebsiteCommand>,
    IBehlogCommandHandler<RemoveWebsiteCommand>,
    IBehlogCommandHandler<SetWebsiteStatusCommand>

{
    private readonly IIdyfaUserContext _userContext;
    private readonly IBehlogApplicationContext _applicationContext;
    private readonly IWebsiteService _service;
    private readonly IWebsiteReadStore _readStore;
    private readonly IWebsiteWriteStore _writeStore;

    public WebsiteCommandHandlers(
        IBehlogManager manager, ISystemDateTime dateTime, ILogger<WebsiteCommandHandlers> logger,
        IIdyfaUserContext userContext, IBehlogApplicationContext applicationContext,
        IWebsiteReadStore readStore, IWebsiteWriteStore writeStore, 
        IWebsiteService service) : base(logger, manager, dateTime)
    {
        _userContext = userContext ?? throw new ArgumentNullException(nameof(userContext));
        _applicationContext = applicationContext ?? throw new ArgumentNullException(nameof(applicationContext));
        _readStore = readStore ?? throw new ArgumentNullException(nameof(readStore));
        _writeStore = writeStore ?? throw new ArgumentNullException(nameof(writeStore));
        _service = service ?? throw new ArgumentNullException(nameof(service));
    }
    
    public async Task<CommandResult<WebsiteResult>> HandleAsync(
        CreateWebsiteCommand command, CancellationToken cancellationToken = default)
    {
        var validation = CreateWebsiteCommandValidator.Run(command);
        if (validation.HasError)
        {
            return CommandResult<WebsiteResult>.WithValidations(validation.Items);
        }

        var website = await Website.CreateAsync(command, _service);
        _writeStore.MarkForAdd(website);
        
        try
        {
            await _writeStore.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
        }
        catch (Exception ex)
        {
            LogException(ex);
            throw;
        }
        finally
        {
            await PublishAsync<Website, Guid>(website, cancellationToken).ConfigureAwait(false);
        }

        return await Task.FromResult(
            CommandResult<WebsiteResult>.With(website.ToResult())
            );
    }


    public async Task<CommandResult> HandleAsync(
        UpdateWebsiteCommand command, CancellationToken cancellationToken = default)
    {
        var validation = UpdateWebsiteCommandValidator.Run(command);
        if (validation.HasError)
        {
            return CommandResult.FromValidator(validation);
        }

        var website = await GetByIdAsync(command.Id, cancellationToken);
        website.ThrowExceptionIfReferenceIsNull(nameof(website));
        await website.UpdateAsync(command, _service, _userContext, _applicationContext);

        try
        {
            await _writeStore.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
        }
        catch (Exception ex)
        {
            LogException(ex);
            throw;
        }
        finally
        {
            await PublishAsync<Website, Guid>(website, cancellationToken).ConfigureAwait(false);
        }
        
        return CommandResult.Create();
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