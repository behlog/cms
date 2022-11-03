using Behlog.Core;
using Behlog.Cms.Store;
using Behlog.Cms.Models;
using Behlog.Extensions;
using Behlog.Cms.Commands;


namespace Behlog.Cms.Domain.FileUploads.Handlers;

public class FileUploadCommandHandlers :
    IBehlogCommandHandler<CreateFileUploadCommand, FileUploadResult>,
    IBehlogCommandHandler<UpdateFileUploadCommand>,
    IBehlogCommandHandler<SoftDeleteFileUploadCommand>,
    IBehlogCommandHandler<RemoveFileUploadCommand>
{
    private readonly IFileUploadReadStore _readStore;
    private readonly IFileUploadWriteStore _writeStore;


    public FileUploadCommandHandlers(
        IFileUploadReadStore readStore, IFileUploadWriteStore writeStore)
    {
        _readStore = readStore ?? throw new ArgumentNullException(nameof(readStore));
        _writeStore = writeStore ?? throw new ArgumentNullException(nameof(writeStore));
    }

    public async Task<FileUploadResult> HandleAsync(
        CreateFileUploadCommand command, CancellationToken cancellationToken = default)
    {
        command.ThrowExceptionIfArgumentIsNull(nameof(command));

        var fileUpload = FileUpload.Create(command);
        _writeStore.MarkForAdd(fileUpload);

        await _writeStore.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
        return fileUpload.ToResult();
    }

    public async Task HandleAsync(UpdateFileUploadCommand command, CancellationToken cancellationToken = default)
    {
        command.ThrowExceptionIfArgumentIsNull(nameof(command));

        var fileUpload = await _readStore.FindAsync(command.Id, cancellationToken).ConfigureAwait(false);
        fileUpload.ThrowExceptionIfReferenceIsNull(nameof(fileUpload));

        fileUpload.Update(command);
        _writeStore.MarkForUpdate(fileUpload);
        await _writeStore.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
    }

    public Task HandleAsync(SoftDeleteFileUploadCommand message, CancellationToken cancellationToken = new CancellationToken())
    {
        throw new NotImplementedException();
    }

    public Task HandleAsync(RemoveFileUploadCommand message, CancellationToken cancellationToken = new CancellationToken())
    {
        throw new NotImplementedException();
    }
}