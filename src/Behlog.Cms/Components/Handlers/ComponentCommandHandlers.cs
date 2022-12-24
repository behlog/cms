using Behlog.Core;
using Behlog.Extensions;
using Behlog.Cms.Domain;
using Behlog.Cms.Models;
using Behlog.Core.Models;
using Behlog.Cms.Commands;
using Idyfa.Core.Contracts;
using Behlog.Cms.Contracts;
using Behlog.Core.Contracts;
using Behlog.Cms.Validations;

namespace Behlog.Cms.Handlers;


public class ComponentCommandHandlers :
    IBehlogCommandHandler<CreateComponentCommand, CommandResult<ComponentResult>>,
    IBehlogCommandHandler<UpdateComponentCommand, CommandResult>,
    IBehlogCommandHandler<SoftDeleteComponentCommand, CommandResult>,
    IBehlogCommandHandler<RemoveComponentCommand, CommandResult>,
    IBehlogCommandHandler<UpsertComponentCommand, CommandResult<ComponentResult>>,
    IBehlogCommandHandler<CreateComponentFilesCommand, CommandResult>
{
    private readonly IIdyfaUserContext _userContext;
    private readonly IBehlogApplicationContext _appContext;
    private readonly ISystemDateTime _dateTime;
    private readonly IComponentService _service;
    private readonly IComponentReadStore _readStore;
    private readonly IComponentWriteStore _writeStore;
    private readonly Behlogger<ComponentCommandHandlers> _behlogger;

    public ComponentCommandHandlers(
        IIdyfaUserContext userContext, IBehlogApplicationContext appContext, 
        ISystemDateTime dateTime, IComponentService service, IComponentReadStore readStore, 
        IComponentWriteStore writeStore, ILogger<ComponentCommandHandlers> logger)
    {
        _userContext = userContext ?? throw new ArgumentNullException(nameof(userContext));
        _appContext = appContext ?? throw new ArgumentNullException(nameof(appContext));
        _dateTime = dateTime ?? throw new ArgumentNullException(nameof(dateTime));
        _service = service ?? throw new ArgumentNullException(nameof(service));
        _readStore = readStore ?? throw new ArgumentNullException(nameof(readStore));
        _writeStore = writeStore ?? throw new ArgumentNullException(nameof(writeStore));

        _behlogger = new Behlogger<ComponentCommandHandlers>(logger, dateTime);
    }

    public async Task<CommandResult<ComponentResult>> HandleAsync(
        CreateComponentCommand command, CancellationToken cancellationToken = default)
    {
        var validation = CreateComponentCommandValidator.Run(command);
        if (validation.HasError)
        {
            return CommandResult<ComponentResult>.Failed(validation.Errors);
        }

        try
        {
            var component = await Component.CreateAsync(
                command, _appContext, _userContext, _dateTime, _service);
            await _writeStore.AddAsync(component, cancellationToken).ConfigureAwait(false);

            return await Task.FromResult(
                CommandResult<ComponentResult>.Create().With(component.ToResult())
            );
        }
        catch (Exception ex)
        {
            _behlogger.LogException(ex);
            throw;
        }
    }

    public async Task<CommandResult> HandleAsync(
        UpdateComponentCommand command, CancellationToken cancellationToken = default)
    {
        var validation = new UpdateComponentCommandValidator().Validate(command);
        if (validation.HasError)
        {
            return CommandResult.Failed(validation.Errors);
        }

        try
        {
            var component = await _readStore.FindAsync(command.Id, cancellationToken).ConfigureAwait(false);
            component.ThrowExceptionIfReferenceIsNull(nameof(component));

            await component.UpdateAsync(command, _appContext, _userContext, _dateTime, _service);
            await _writeStore.UpdateAsync(component, cancellationToken).ConfigureAwait(false);
        }
        catch (Exception ex)
        {
            _behlogger.LogException(ex);
            throw;
        }
        
        return CommandResult.Success();
    }

    public async Task<CommandResult> HandleAsync(
        SoftDeleteComponentCommand command, CancellationToken cancellationToken = default)
    {
        command.ThrowExceptionIfArgumentIsNull(nameof(command));

        var component = await _readStore.FindAsync(command.Id, cancellationToken).ConfigureAwait(false);
        component.ThrowExceptionIfReferenceIsNull(nameof(component));

        component.SoftDelete(_userContext, _appContext, _dateTime);
        await _writeStore.UpdateAsync(component, cancellationToken).ConfigureAwait(false);
        
        return CommandResult.Success();
    }

    public async Task<CommandResult> HandleAsync(
        RemoveComponentCommand command, CancellationToken cancellationToken = default)
    {
        command.ThrowExceptionIfArgumentIsNull(nameof(command));

        var component = await _readStore.FindAsync(command.Id, cancellationToken).ConfigureAwait(false);
        component.ThrowExceptionIfReferenceIsNull(nameof(component));
        
        component.Remove();
        await _writeStore.DeleteAsync(component, cancellationToken).ConfigureAwait(false);
        
        return CommandResult.Success();
    }

    public async Task<CommandResult<ComponentResult>> HandleAsync(
        UpsertComponentCommand command, CancellationToken cancellationToken = default)
    {
        var validation = UpsertComponentCommandValidator.Run(command);
        if (validation.HasError)
        {
            return CommandResult<ComponentResult>.Failed(validation.Errors);
        }

        var existingComponent = await _readStore.GetByNameAsync(
            command.WebsiteId, command.LangId, command.Name, cancellationToken).ConfigureAwait(false);
        if (existingComponent is null)
        {
            return await HandleAsync(command.ConvertToCreateCommand(), cancellationToken);
        }
        else
        {
            var updateCommand = command.ConvertToUpdateCommand(existingComponent.Id);
            await existingComponent.UpdateAsync(updateCommand, _appContext, _userContext, _dateTime, _service);
            await _writeStore.UpdateAsync(existingComponent, cancellationToken).ConfigureAwait(false);

            return CommandResult<ComponentResult>
                .Create()
                .With(existingComponent.ToResult());
        }
    }

    public async Task<CommandResult> HandleAsync(
        CreateComponentFilesCommand command, CancellationToken cancellationToken = default)
    {
        command.ThrowExceptionIfArgumentIsNull(nameof(command));

        var component = await _readStore.GetByIdAsync(command.ComponentId, cancellationToken).ConfigureAwait(false);
        component.ThrowExceptionIfReferenceIsNull(nameof(component));

        var files = command.Files.Select(
            _ => new ComponentFileCommand(_.FileId, _.FileName, _.Title, _.Description)
            ).ToList();
        component.AddFiles(files, _userContext, _appContext, _dateTime);

        await _writeStore.UpdateAsync(component, cancellationToken).ConfigureAwait(false);
        
        return CommandResult.Success();
    }
}