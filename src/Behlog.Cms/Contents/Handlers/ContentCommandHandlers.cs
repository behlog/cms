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


public class ContentCommandHandlers :
    IBehlogCommandHandler<CreateContentCommand, CommandResult<ContentResult>>,
    IBehlogCommandHandler<UpdateContentCommand, CommandResult>,
    IBehlogCommandHandler<SoftDeleteContentCommand>,
    IBehlogCommandHandler<PublishContentCommand, CommandResult>,
    IBehlogCommandHandler<RemoveContentCommand>
{
    private readonly IIdyfaUserContext _userContext;
    private readonly IBehlogApplicationContext _appContext;
    private readonly IContentReadStore _readStore;
    private readonly IContentWriteStore _writeStore;
    private readonly IContentService _service;
    private readonly ISystemDateTime _dateTime;
    private readonly Behlogger<ContentCommandHandlers> _behlogger;

    
    public ContentCommandHandlers(
        ILogger<ContentCommandHandlers> logger, IIdyfaUserContext userContext, IContentReadStore readStore, 
        IContentWriteStore writeStore, IBehlogApplicationContext appContext, IContentService contentService, 
        ISystemDateTime dateTime)
    {
        _service = contentService ?? throw new ArgumentNullException(nameof(contentService));
        _appContext = appContext ?? throw new ArgumentNullException(nameof(appContext));
        _userContext = userContext ?? throw new ArgumentNullException(nameof(userContext));
        _readStore = readStore ?? throw new ArgumentNullException(nameof(readStore));
        _writeStore = writeStore ?? throw new ArgumentNullException(nameof(writeStore));
        _dateTime = dateTime ?? throw new ArgumentNullException(nameof(dateTime));
        _behlogger = new Behlogger<ContentCommandHandlers>(logger, dateTime);
    }

    
    public async Task<CommandResult<ContentResult>> HandleAsync(
        CreateContentCommand command, CancellationToken cancellationToken = default)
    {
        var validation = CreateContentCommandValidator.Run(command);
        if (validation.HasError)
        {
            return CommandResult<ContentResult>.Failed(validation.Errors);
        }

        try
        {
            var content = await Content.CreateAsync(
                command, _service, _userContext, _appContext, _dateTime);
            await _writeStore.AddAsync(content, cancellationToken).ConfigureAwait(false);
            
            return await Task.FromResult(
                CommandResult<ContentResult>.Create().With(content.ToResult())
            );
        }
        catch (Exception ex)
        {
            _behlogger.LogException(ex);
            throw;
        }
    }

    
    public async Task<CommandResult> HandleAsync(
        UpdateContentCommand command, CancellationToken cancellationToken = default)
    {
        var validation = UpdateContentCommandValidator.Run(command);
        if (validation.HasError)
        {
            return CommandResult.Failed(validation.Errors);
        }

        try
        {
            var content = await _readStore.FindAsync(command.Id, cancellationToken).ConfigureAwait(false);
            content.ThrowExceptionIfReferenceIsNull(nameof(content));

            await content.UpdateAsync(
                command, _service, _userContext, _dateTime, _appContext);
        
            await _writeStore.UpdateAsync(content, cancellationToken).ConfigureAwait(false);
        }
        catch (Exception ex)
        {
            _behlogger.LogException(ex);
            throw;
        }
        
        return CommandResult.Success();
    }

    public async Task HandleAsync(
        SoftDeleteContentCommand command, CancellationToken cancellationToken = default)
    {
        command.ThrowExceptionIfArgumentIsNull(nameof(command));

        var content = await _readStore.FindAsync(command.Id, cancellationToken).ConfigureAwait(false);
        content.ThrowExceptionIfReferenceIsNull(nameof(content));
        await content.SoftDeleteAsync(_userContext, _appContext, _dateTime);
        
        _writeStore.MarkForUpdate(content);
        await _writeStore.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
    }
    
    public async Task<CommandResult> HandleAsync(
        PublishContentCommand command, CancellationToken cancellationToken = default)
    {
        command.ThrowExceptionIfArgumentIsNull(nameof(command));

        var content = await _readStore.FindAsync(command.Id, cancellationToken).ConfigureAwait(false);
        content.ThrowExceptionIfReferenceIsNull(nameof(content));
        await content.PublishContentAsync(_userContext, _dateTime, _appContext);

        _writeStore.MarkForUpdate(content);
        await _writeStore.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
        
        return CommandResult.Success();
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