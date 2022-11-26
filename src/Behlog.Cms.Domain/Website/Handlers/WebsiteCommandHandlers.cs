using Behlog.Core;
using Behlog.Cms.Store;
using Behlog.Extensions;
using Behlog.Cms.Domain;
using Behlog.Cms.Models;
using Behlog.Core.Models;
using Behlog.Cms.Commands;
using Behlog.Cms.Contracts;
using Idyfa.Core.Contracts;
using Behlog.Core.Contracts;
using Microsoft.Extensions.Logging;

namespace Behlog.Cms.Handlers;


public class WebsiteCommandHandlers :
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
    private readonly ILogger<WebsiteCommandHandlers> _logger;
    private readonly ISystemDateTime _dateTime;
    private readonly IBehlogMediator _mediator;

    private readonly Behlogger<WebsiteCommandHandlers> _behlogger;

    public WebsiteCommandHandlers(
        IBehlogMediator mediator, ISystemDateTime dateTime, ILogger<WebsiteCommandHandlers> logger,
        IIdyfaUserContext userContext, IBehlogApplicationContext applicationContext,
        IWebsiteReadStore readStore, IWebsiteWriteStore writeStore, 
        IWebsiteService service)
    {
        _userContext = userContext ?? throw new ArgumentNullException(nameof(userContext));
        _applicationContext = applicationContext ?? throw new ArgumentNullException(nameof(applicationContext));
        _readStore = readStore ?? throw new ArgumentNullException(nameof(readStore));
        _writeStore = writeStore ?? throw new ArgumentNullException(nameof(writeStore));
        _service = service ?? throw new ArgumentNullException(nameof(service));
        _dateTime = dateTime ?? throw new ArgumentNullException(nameof(dateTime));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));

        _behlogger = new Behlogger<WebsiteCommandHandlers>(_logger, _dateTime);
    }
    
    public async Task<CommandResult<WebsiteResult>> HandleAsync(
        CreateWebsiteCommand command, CancellationToken cancellationToken = default)
    {
        var validation = CreateWebsiteCommandValidator.Run(command);
        if (validation.HasError)
        {
            return CommandResult<WebsiteResult>.FromValidator(validation);
        }

        var website = await Website.CreateAsync(command, _service);
        _writeStore.MarkForAdd(website);
        
        try
        {
            await _writeStore.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
        }
        catch (Exception ex)
        {
            _behlogger.LogException(ex);
            throw;
        }

        return await Task.FromResult(
            CommandResult<WebsiteResult>.Create().With(website.ToResult())
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
            _behlogger.LogException(ex);
            throw;
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

        try
        {
            await _writeStore.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
        }
        catch (Exception ex)
        {
            _behlogger.LogException(ex);
            throw;
        }
    }

    public async Task HandleAsync(
        RemoveWebsiteCommand command, CancellationToken cancellationToken = default)
    {
        command.ThrowExceptionIfArgumentIsNull(nameof(command));

        var website = await GetByIdAsync(command.Id, cancellationToken);
        website.Remove();
        _writeStore.MarkForDelete(website);

        try
        {
            await _writeStore.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
        }
        catch (Exception ex)
        {
            _behlogger.LogException(ex);
            throw;
        }
    }

    public async Task HandleAsync(
        SetWebsiteStatusCommand command, CancellationToken cancellationToken = default)
    {
        command.ThrowExceptionIfArgumentIsNull(nameof(command));

        var website = await GetByIdAsync(command.Id, cancellationToken);
        website.ThrowExceptionIfReferenceIsNull(nameof(website));
        
        website.SetStatus(command.Status, _userContext, _applicationContext);

        try
        {
            await _writeStore.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
        }
        catch (Exception ex)
        {
            _behlogger.LogException(ex);
            throw;
        }
    }


    #region private helpers

    private async Task<Website> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _readStore.FindAsync(id, cancellationToken).ConfigureAwait(false);
    }

    #endregion
}