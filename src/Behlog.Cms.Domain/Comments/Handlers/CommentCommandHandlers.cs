using Behlog.Cms.Commands;
using Behlog.Cms.Domain;
using Behlog.Cms.Domain.Models;
using Behlog.Cms.Models;
using Behlog.Cms.Store;
using Behlog.Core;
using Behlog.Core.Contracts;
using Behlog.Extensions;
using Idyfa.Core.Contracts;

namespace Behlog.Cms.Handlers;


public class CommentCommandHandlers :
    IBehlogCommandHandler<CreateCommentCommand, CommentCommandResult>,
    IBehlogCommandHandler<UpdateCommentCommand>
{
    
    private readonly ICommentWriteStore _writeStore;
    private readonly ICommentReadStore _readStore;
    private readonly IBehlogApplicationContext _applicationContext;
    private readonly IIdyfaUserContext _userContext;

    public CommentCommandHandlers(
        ICommentWriteStore writeStore, ICommentReadStore readStore, 
        IBehlogApplicationContext applicationContext, IIdyfaUserContext userContext)
    {
        _writeStore = writeStore ?? throw new ArgumentNullException(nameof(writeStore));
        _readStore = readStore ?? throw new ArgumentNullException(nameof(readStore));
        _applicationContext = applicationContext ?? throw new ArgumentNullException(nameof(applicationContext));
        _userContext = userContext ?? throw new ArgumentNullException(nameof(userContext));
    }


    public async Task<CommentCommandResult> HandleAsync(
        CreateCommentCommand command, CancellationToken cancellationToken = default)
    {
        command.ThrowExceptionIfArgumentIsNull(nameof(command));

        var comment = Comment.Create(command);
        comment.SetIdentityOnAdd(_userContext, _applicationContext);
        _writeStore.MarkForAdd(comment);
        await _writeStore.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

        // return new CommentCommandResult(comment.ToResult());
        return new CommentCommandResult();
    }

    public async Task HandleAsync(
        UpdateCommentCommand command, CancellationToken cancellationToken = default)
    {
        command.ThrowExceptionIfArgumentIsNull(nameof(command));

        var comment = await _readStore.FindAsync(command.Id, cancellationToken).ConfigureAwait(false);
        comment.Update(command);
        comment.SetIdentityOnUpdate(_userContext, _applicationContext);
        
        _writeStore.MarkForUpdate(comment);
        await _writeStore.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
    }
}