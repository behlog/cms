using Behlog.Core;
using Behlog.Cms.Store;
using Behlog.Cms.Domain;
using Behlog.Extensions;
using Behlog.Cms.Models;
using Behlog.Core.Models;
using Behlog.Cms.Commands;
using Behlog.Cms.Contracts;
using Behlog.Cms.Validations;
using Behlog.Core.Contracts;

namespace Behlog.Cms.Handlers;

/// <summary>
/// Handlers for <see cref="ContentType"/> commands. 
/// </summary>
public class ContentTypeCommandHandler :
    IBehlogCommandHandler<CreateContentTypeCommand, CommandResult<ContentTypeResult>>,
    IBehlogCommandHandler<UpdateContentTypeCommand, CommandResult>,
    IBehlogCommandHandler<SoftDeleteContentTypeCommand>
{
    private readonly ISystemDateTime _dateTime;
    private readonly IContentTypeReadStore _readStore;
    private readonly IContentTypeWriteStore _writeStore;
    private readonly IContentTypeService _service;
    private readonly ILogger<ContentTypeCommandHandler> _logger;
    private readonly Behlogger<ContentTypeCommandHandler> _behlogger;

    public ContentTypeCommandHandler(
        IContentTypeReadStore readStore, IContentTypeWriteStore writeStore, ISystemDateTime dateTime, 
        ILogger<ContentTypeCommandHandler> logger, IContentTypeService service)
    {
        _readStore = readStore ?? throw new ArgumentNullException(nameof(readStore));
        _writeStore = writeStore ?? throw new ArgumentNullException(nameof(writeStore));
        _dateTime = dateTime ?? throw new ArgumentNullException(nameof(dateTime));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _service = service ?? throw new ArgumentNullException(nameof(service));
        _behlogger = new Behlogger<ContentTypeCommandHandler>(logger, dateTime);
    }


    public async Task<CommandResult<ContentTypeResult>> HandleAsync(
        CreateContentTypeCommand command, CancellationToken cancellationToken = default)
    {
        var validation = CreateContentTypeCommandValidator.Run(command);
        if (validation.HasError)
        {
            _behlogger.LogError(validation.ToString()!);
            return CommandResult<ContentTypeResult>.Failed(validation.Errors);
        }
        
        try
        {
            var contentType = await ContentType.CreateAsync(command, _dateTime, _service);
            await _writeStore.AddAsync(contentType, cancellationToken).ConfigureAwait(false);

            return await Task.FromResult(CommandResult<ContentTypeResult>
                .Success(contentType.ToResult())
            );
        }
        catch (Exception ex)
        {
            _behlogger.LogException(ex);
            throw;
        }
    }

    
    public async Task<CommandResult> HandleAsync(
        UpdateContentTypeCommand command, CancellationToken cancellationToken = default)
    {
        var validation = UpdateContentTypeCommandValidator.Run(command);
        if (validation.HasError)
        {
            _behlogger.LogError(validation.ToString());
            return CommandResult.Failed(validation.Errors);
        }

        try
        {
            var contentType = await _readStore.FindAsync(command.Id, cancellationToken).ConfigureAwait(false);
            contentType.ThrowExceptionIfReferenceIsNull(nameof(contentType));
            
            contentType.Update(command, _dateTime);
            _writeStore.MarkForUpdate(contentType);
            await _writeStore.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
        }
        catch (Exception ex)
        {
            _behlogger.LogException(ex);
            throw;
        }

        return CommandResult.Success();
    }

    public async Task HandleAsync(
        SoftDeleteContentTypeCommand command, CancellationToken cancellationToken = default)
    {
        command.ThrowExceptionIfArgumentIsNull(nameof(command));

        var contentType = await _readStore.FindAsync(command.Id, cancellationToken).ConfigureAwait(false);
        contentType.ThrowExceptionIfReferenceIsNull(nameof(contentType));
        
        contentType.SoftDelete();
        _writeStore.MarkForUpdate(contentType);
        await _writeStore.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
    }
    
}