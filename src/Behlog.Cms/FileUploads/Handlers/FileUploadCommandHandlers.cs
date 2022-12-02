using Behlog.Core;
using Behlog.Cms.Store;
using Behlog.Extensions;
using Behlog.Cms.Models;
using Behlog.Cms.Domain;
using Behlog.Core.Models;
using Behlog.Cms.Commands;
using Idyfa.Core.Contracts;
using Behlog.Core.Contracts;
using Behlog.Cms.FileUploads.Internal;

namespace Behlog.Cms.Handlers;


public class FileUploadCommandHandlers :
    IBehlogCommandHandler<CreateFileUploadCommand, FileUploadResult>,
    IBehlogCommandHandler<UpdateFileUploadCommand>,
    IBehlogCommandHandler<SoftDeleteFileUploadCommand>,
    IBehlogCommandHandler<RemoveFileUploadCommand>
{
    private readonly IFileUploadReadStore _readStore;
    private readonly IFileUploadWriteStore _writeStore;
    private readonly IIdyfaUserContext _userContext;
    private readonly IBehlogApplicationContext _appContext;
    private readonly ISystemDateTime _dateTime;
    private readonly FileUploader _uploader;
    
    public FileUploadCommandHandlers(
        IFileUploadReadStore readStore, IFileUploadWriteStore writeStore, 
        IIdyfaUserContext userContext, IBehlogApplicationContext appContext, 
        IWebHostEnvironment env, ISystemDateTime dateTime, BehlogOptions options)
    {
        _readStore = readStore ?? throw new ArgumentNullException(nameof(readStore));
        _writeStore = writeStore ?? throw new ArgumentNullException(nameof(writeStore));
        _userContext = userContext ?? throw new ArgumentNullException(nameof(userContext));
        _appContext = appContext ?? throw new ArgumentNullException(nameof(appContext));
        _dateTime = dateTime ?? throw new ArgumentNullException(nameof(dateTime));
        _uploader = new FileUploader(options, env);
    }

    public async Task<FileUploadResult> HandleAsync(
        CreateFileUploadCommand command, CancellationToken cancellationToken = default)
    {
        command.ThrowExceptionIfArgumentIsNull(nameof(command));

        var uploadResult = await _uploader.UploadAsync(
            command.FileData, command.ContentType, command.FileType);
        FileUploaderResult? alternateUploadResult = null;
        if (command.AlternateFileData.IsNotNullOrEmpty())
        {
            alternateUploadResult = await _uploader.UploadAsync(
                command.AlternateFileData, command.ContentType, command.FileType);
        }
        
        var fileUpload = FileUpload.Create(
            command, uploadResult, alternateUploadResult, _appContext, _userContext);
        
        _writeStore.MarkForAdd(fileUpload);
        await _writeStore.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
        
        return fileUpload.ToResult();
    }

    public async Task HandleAsync(
        UpdateFileUploadCommand command, CancellationToken cancellationToken = default)
    {
        command.ThrowExceptionIfArgumentIsNull(nameof(command));

        var fileUpload = await _readStore.FindAsync(command.Id, cancellationToken).ConfigureAwait(false);
        fileUpload.ThrowExceptionIfReferenceIsNull(nameof(fileUpload));

        fileUpload.Update(command);
        _writeStore.MarkForUpdate(fileUpload);
        await _writeStore.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
    }

    public async Task HandleAsync(
        SoftDeleteFileUploadCommand command, CancellationToken cancellationToken = default)
    {
        command.ThrowExceptionIfArgumentIsNull(nameof(command));
        var fileUpload = await _readStore.FindAsync(command.Id, cancellationToken).ConfigureAwait(false);
        fileUpload.ThrowExceptionIfReferenceIsNull(nameof(fileUpload));
        
        fileUpload.SoftDelete();
        await _writeStore.SaveChangesAsync(cancellationToken);
    }

    public async Task HandleAsync(
        RemoveFileUploadCommand command, CancellationToken cancellationToken = default)
    {
        command.ThrowExceptionIfArgumentIsNull(nameof(command));
        var fileUpload = await _readStore.FindAsync(command.Id, cancellationToken).ConfigureAwait(false);
        fileUpload.ThrowExceptionIfReferenceIsNull(nameof(fileUpload));
        
        fileUpload.Remove();
        _writeStore.MarkForDelete(fileUpload);
        await _writeStore.SaveChangesAsync(cancellationToken);
    }
}