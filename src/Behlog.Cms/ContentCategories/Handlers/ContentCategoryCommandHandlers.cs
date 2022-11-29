using Behlog.Core;
using Behlog.Cms.Store;
using Behlog.Extensions;
using Behlog.Cms.Domain;
using Behlog.Cms.Models;
using Behlog.Cms.Commands;
using Behlog.Cms.Validations;
using Behlog.Core.Contracts;
using Behlog.Core.Models;
using Idyfa.Core.Contracts;
using Microsoft.Extensions.Logging;

namespace Behlog.Cms.Handlers;


public class ContentCategoryCommandHandlers :
    IBehlogCommandHandler<CreateContentCategoryCommand, CommandResult<ContentCategoryResult>>,
    IBehlogCommandHandler<UpdateContentCategoryCommand, CommandResult>,
    IBehlogCommandHandler<SoftDeleteContentCategoryCommand>,
    IBehlogCommandHandler<RemoveContentCategoryCommand>
{
    private readonly IContentCategoryWriteStore _writeStore;
    private readonly IContentCategoryReadStore _readStore;
    private readonly IBehlogApplicationContext _appContext;
    private readonly IIdyfaUserContext _userContext;
    private readonly ISystemDateTime _dateTime;
    private readonly ILogger<ContentCategoryCommandHandlers> _logger;
    private readonly Behlogger<ContentCategoryCommandHandlers> _behlogger;

    public ContentCategoryCommandHandlers(
        IContentCategoryWriteStore writeStore, IContentCategoryReadStore readStore,
        IBehlogApplicationContext appContext, IIdyfaUserContext userContext, ISystemDateTime dateTime,
        ILogger<ContentCategoryCommandHandlers> logger)
    {
        _writeStore = writeStore ?? throw new ArgumentNullException(nameof(writeStore));
        _readStore = readStore ?? throw new ArgumentNullException(nameof(readStore));
        _appContext = appContext ?? throw new ArgumentNullException(nameof(appContext));
        _userContext = userContext ?? throw new ArgumentNullException(nameof(userContext));
        _dateTime = dateTime ?? throw new ArgumentNullException(nameof(dateTime));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));

        _behlogger = new Behlogger<ContentCategoryCommandHandlers>(_logger, _dateTime);
    }
    
    
    public async Task<CommandResult<ContentCategoryResult>> HandleAsync(
        CreateContentCategoryCommand command, CancellationToken cancellationToken = default)
    {
        var validation = CreateContentCategoryCommandValidator.Run(command);
        if (validation.HasError)
        {
            return CommandResult<ContentCategoryResult>.Failed(validation.Errors);
        }

        try
        {
            var category = ContentCategory.Create(command, _userContext, _appContext, _dateTime);
            await _writeStore.AddAsync(category, cancellationToken).ConfigureAwait(false);

            return await Task.FromResult(
                CommandResult<ContentCategoryResult>.Create().With(category.ToResult())
                );
        }
        catch (Exception ex)
        {
            _behlogger.LogException(ex);
            throw;
        }
    }

    
    
    public async Task<CommandResult> HandleAsync(
        UpdateContentCategoryCommand command, CancellationToken cancellationToken = default)
    {
        var validation = UpdateContentCategoryCommandValidator.Run(command);
        if (validation.HasError)
        {
            return CommandResult.Failed(validation.Errors);
        }

        try
        {
            var category = await _readStore.FindAsync(command.Id, cancellationToken).ConfigureAwait(false);
            category.ThrowExceptionIfReferenceIsNull(nameof(category));
        
            category.Update(command, _userContext, _appContext, _dateTime);
            _writeStore.MarkForUpdate(category);
        
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
        SoftDeleteContentCategoryCommand command, CancellationToken cancellationToken = default)
    {
        command.ThrowExceptionIfArgumentIsNull(nameof(command));

        var category = await _readStore.FindAsync(command.Id, cancellationToken).ConfigureAwait(false);
        category.ThrowExceptionIfReferenceIsNull(nameof(category));
        
        category.SoftDelete();
        
        
        _writeStore.MarkForUpdate(category);
        await _writeStore.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
    }


    public async Task HandleAsync(
        RemoveContentCategoryCommand command, CancellationToken cancellationToken = default)
    {
        command.ThrowExceptionIfArgumentIsNull(nameof(command));

        var category = await _readStore.FindAsync(command.Id, cancellationToken).ConfigureAwait(false);
        category.ThrowExceptionIfReferenceIsNull(nameof(category));

        await _writeStore.DeleteAsync(category, cancellationToken).ConfigureAwait(false);
    }
}