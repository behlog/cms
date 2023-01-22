namespace Behlog.Cms.Handlers;

public class TagCommandsHandlers :
    IBehlogCommandHandler<CreateTagCommand, CommandResult<TagResult>>,
    IBehlogCommandHandler<RemoveTagCommand>,
    IBehlogCommandHandler<SoftDeleteTagCommand>
{
    private readonly ITagReadStore _readStore;
    private readonly ITagWriteStore _writeStore;
    private readonly IBehlogApplicationContext _appContext;
    private readonly IIdyfaUserContext _userContext;
    private readonly ISystemDateTime _dateTime;
    private readonly ILogger<TagCommandsHandlers> _logger;
    private readonly Behlogger<TagCommandsHandlers> _behlogger;
    
    public async Task<CommandResult<TagResult>> HandleAsync(
        CreateTagCommand command, CancellationToken cancellationToken = default)
    {
        var validation = CreateTagCommandValidator.Run(command);
        if (validation.HasError)
        {
            return CommandResult<TagResult>.Failed(validation.Errors);
        }

        try
        {
            var tag = Tag.Create(command, _appContext, _userContext, _dateTime);
            await _writeStore.AddAsync(tag, cancellationToken).ConfigureAwait(false);

            return await Task.FromResult(
                CommandResult<TagResult>.Create().With(tag.ToResult())
                );
        }
        catch (Exception ex)
        {
            _behlogger.LogException(ex);
            throw;
        }
    }

    public async Task HandleAsync(
        RemoveTagCommand message, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public async Task HandleAsync(
        SoftDeleteTagCommand message, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}