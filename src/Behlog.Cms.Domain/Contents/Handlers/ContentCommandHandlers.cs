using Behlog.Cms.Commands;
using Behlog.Cms.Domain;
using Behlog.Cms.Models;
using Behlog.Cms.Validations;
using Behlog.Cms.Contracts;
using Behlog.Core;
using Behlog.Core.Contracts;
using Behlog.Core.Models;
using Behlog.Core.Validations;
using Behlog.Extensions;
using Idyfa.Core.Contracts;
using Microsoft.Extensions.Logging;

namespace Behlog.Cms.Handlers;


public class ContentCommandHandlers :
    IBehlogCommandHandler<CreateContentCommand, CommandResult<ContentResult>>,
    IBehlogCommandHandler<UpdateContentCommand>,
    IBehlogCommandHandler<SoftDeleteContentCommand>,
    IBehlogCommandHandler<PublishContentCommand, ValidationResult>,
    IBehlogCommandHandler<RemoveContentCommand>
{
    private readonly IBehlogMediator _mediator;
    private readonly IBehlogMediatorAssistant _assistant;
    private readonly IIdyfaUserContext _userContext;
    private readonly IBehlogApplicationContext _appContext;
    private readonly IContentReadStore _readStore;
    private readonly IContentWriteStore _writeStore;
    private readonly IContentService _service;
    private readonly ISystemDateTime _dateTime;
    private readonly ILogger<ContentCommandHandlers> _logger;

    public ContentCommandHandlers(
        ILogger<ContentCommandHandlers> logger, IBehlogMediator mediator, IBehlogMediatorAssistant assistant,
        IIdyfaUserContext userContext, IContentReadStore readStore, IContentWriteStore writeStore, 
        IBehlogApplicationContext appContext, IContentService contentService, ISystemDateTime dateTime)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _assistant = assistant ?? throw new ArgumentNullException(nameof(assistant));
        _service = contentService ?? throw new ArgumentNullException(nameof(contentService));
        _appContext = appContext ?? throw new ArgumentNullException(nameof(appContext));
        _userContext = userContext ?? throw new ArgumentNullException(nameof(userContext));
        _readStore = readStore ?? throw new ArgumentNullException(nameof(readStore));
        _writeStore = writeStore ?? throw new ArgumentNullException(nameof(writeStore));
        _dateTime = dateTime ?? throw new ArgumentNullException(nameof(dateTime));
    }

    public async Task<CommandResult<ContentResult>> HandleAsync(
        CreateContentCommand command, CancellationToken cancellationToken = default)
    {
        var validation = CreateContentCommandValidator.Run(command);
        if (!validation.IsValid)
        {
            return CommandResult<ContentResult>.FromValidator(validation);
        }

        var content = await Content.CreateAsync(
            command, _service, _userContext, _appContext, _dateTime);

        try
        {
            await _writeStore.AddAsync(content, cancellationToken).ConfigureAwait(false);
        }
        catch (Exception ex)
        {
            _assistant.LogException(ex);
            throw;
        }
        finally
        {
            await _assistant.PublishAsync<Content, Guid>(content, cancellationToken).ConfigureAwait(false);
        }
        
        return await Task.FromResult(
            CommandResult<ContentResult>.Create().With(content.ToResult())
            );
    }

    public async Task HandleAsync(
        UpdateContentCommand command, CancellationToken cancellationToken = default)
    {
        var content = await _readStore.FindAsync(command.Id, cancellationToken).ConfigureAwait(false);
        content.ThrowExceptionIfReferenceIsNull(nameof(content));

        await content.UpdateAsync(
            command, _service, _userContext, _dateTime, _appContext);
        
        await _writeStore.UpdateAsync(content, cancellationToken).ConfigureAwait(false);
    }

    public async Task HandleAsync(
        SoftDeleteContentCommand command, CancellationToken cancellationToken = default)
    {
        command.ThrowExceptionIfArgumentIsNull(nameof(command));

        var content = await _readStore.FindAsync(command.Id, cancellationToken).ConfigureAwait(false);
        content.ThrowExceptionIfReferenceIsNull(nameof(content));
        await content.SoftDeleteAsync(_userContext, _dateTime);
        
        _writeStore.MarkForUpdate(content);
        await _writeStore.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
    }
    
    public async Task<ValidationResult> HandleAsync(
        PublishContentCommand command, CancellationToken cancellationToken = default)
    {
        command.ThrowExceptionIfArgumentIsNull(nameof(command));

        var content = await _readStore.FindAsync(command.Id, cancellationToken).ConfigureAwait(false);
        content.ThrowExceptionIfReferenceIsNull(nameof(content));
        await content.PublishContentAsync(_userContext, _dateTime, _appContext);

        _writeStore.MarkForUpdate(content);
        await _writeStore.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

        return ValidationResult.Create().Build();
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