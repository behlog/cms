using Behlog.Cms.Commands;
using Behlog.Cms.Domain;
using Behlog.Cms.Domain.Models;
using Behlog.Cms.Models;
using Behlog.Cms.Store;
using Behlog.Core;
using Behlog.Core.Contracts;
using Behlog.Core.Models;
using Behlog.Extensions;
using Idyfa.Core.Contracts;

namespace Behlog.Cms.Handlers;


public class CommentCommandHandlers :
    IBehlogCommandHandler<CreateCommentCommand, CommandResult<CommentResult>>,
    IBehlogCommandHandler<UpdateCommentCommand, CommandResult>
{
    
    private readonly ICommentWriteStore _writeStore;
    private readonly ICommentReadStore _readStore;
    private readonly IBehlogApplicationContext _applicationContext;
    private readonly IIdyfaUserContext _userContext;
    private readonly ISystemDateTime _dateTime;

    public CommentCommandHandlers(
        ICommentWriteStore writeStore, ICommentReadStore readStore, 
        IBehlogApplicationContext applicationContext, IIdyfaUserContext userContext, ISystemDateTime dateTime)
    {
        _writeStore = writeStore ?? throw new ArgumentNullException(nameof(writeStore));
        _readStore = readStore ?? throw new ArgumentNullException(nameof(readStore));
        _applicationContext = applicationContext ?? throw new ArgumentNullException(nameof(applicationContext));
        _userContext = userContext ?? throw new ArgumentNullException(nameof(userContext));
        _dateTime = dateTime ?? throw new ArgumentNullException(nameof(dateTime));
    }


    public async Task<CommandResult<CommentResult>> HandleAsync(
        CreateCommentCommand command, CancellationToken cancellationToken = default)
    {
        command.ThrowExceptionIfArgumentIsNull(nameof(command));

        var comment = Comment.Create(command, _applicationContext, _userContext, _dateTime);
        _writeStore.MarkForAdd(comment);
        await _writeStore.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

        // return new CommentCommandResult(comment.ToResult());
        throw new NotImplementedException();
    }

    public async Task<CommandResult> HandleAsync(
        UpdateCommentCommand command, CancellationToken cancellationToken = default)
    {
        command.ThrowExceptionIfArgumentIsNull(nameof(command));

        var comment = await _readStore.FindAsync(command.Id, cancellationToken).ConfigureAwait(false);
        comment.Update(command, _applicationContext, _userContext, _dateTime);
        
        _writeStore.MarkForUpdate(comment);
        await _writeStore.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

        return CommandResult.Success();
    }
}