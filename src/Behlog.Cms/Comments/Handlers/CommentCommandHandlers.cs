using Behlog.Cms.Commands;
using Behlog.Cms.Domain;
using Behlog.Cms.Models;
using Behlog.Cms.Validations;
using Behlog.Core;
using Behlog.Core.Contracts;
using Behlog.Core.Models;
using Behlog.Extensions;
using Idyfa.Core.Contracts;

namespace Behlog.Cms.Handlers;


public class CommentCommandHandlers :
    IBehlogCommandHandler<CreateCommentCommand, CommandResult<CommentResult>>,
    IBehlogCommandHandler<UpdateCommentCommand, CommandResult>,
    IBehlogCommandHandler<ApproveCommentCommand>
{
    
    private readonly ICommentWriteStore _writeStore;
    private readonly ICommentReadStore _readStore;
    private readonly IBehlogApplicationContext _applicationContext;
    private readonly IIdyfaUserContext _userContext;
    private readonly ISystemDateTime _dateTime;
    private readonly Behlogger<CommentCommandHandlers> _behlogger;

    public CommentCommandHandlers(
        ICommentWriteStore writeStore, ICommentReadStore readStore, ILogger<CommentCommandHandlers> logger,
        IBehlogApplicationContext applicationContext, IIdyfaUserContext userContext, ISystemDateTime dateTime)
    {
        _writeStore = writeStore ?? throw new ArgumentNullException(nameof(writeStore));
        _readStore = readStore ?? throw new ArgumentNullException(nameof(readStore));
        _applicationContext = applicationContext ?? throw new ArgumentNullException(nameof(applicationContext));
        _userContext = userContext ?? throw new ArgumentNullException(nameof(userContext));
        _dateTime = dateTime ?? throw new ArgumentNullException(nameof(dateTime));
        _behlogger = new Behlogger<CommentCommandHandlers>(logger, dateTime);
    }


    public async Task<CommandResult<CommentResult>> HandleAsync(
        CreateCommentCommand command, CancellationToken cancellationToken = default)
    {
        var validation = CreateCommentCommandValidator.Run(command);
        if (validation.HasError)
        {
            return CommandResult<CommentResult>.Failed(validation.Errors);
        }

        var comment = Comment.Create(command, _applicationContext, _userContext, _dateTime);
        
        try
        {
            _writeStore.MarkForAdd(comment);
            await _writeStore.SaveChangesAsync(cancellationToken).ConfigureAwait(false);    
        }
        catch (Exception ex)
        {
            _behlogger.LogException(ex);
            throw;
        }

        return CommandResult<CommentResult>.Success(comment.ToResult());
    }

    public async Task<CommandResult> HandleAsync(
        UpdateCommentCommand command, CancellationToken cancellationToken = default)
    {
        var validation = UpdateCommentCommandValidator.Run(command);
        if (validation.HasError)
        {
            return CommandResult.Failed(validation.Errors);
        }

        try
        {
            var comment = await _readStore.FindAsync(command.Id, cancellationToken).ConfigureAwait(false);
            comment.Update(command, _applicationContext, _userContext, _dateTime);
            
            _writeStore.MarkForUpdate(comment);
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
        ApproveCommentCommand command, CancellationToken cancellationToken = default)
    {
        command.ThrowExceptionIfArgumentIsNull(nameof(command));

        try
        {
            var comment = await _readStore.FindAsync(command.Id, cancellationToken).ConfigureAwait(false);
            comment.Approve(_applicationContext, _userContext, _dateTime);

            await _writeStore.UpdateAsync(comment, cancellationToken).ConfigureAwait(false);
        }
        catch (Exception ex)
        {
            _behlogger.LogException(ex);
            throw;
        }
    }
    
    
    
}