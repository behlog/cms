using Behlog.Cms.FileUploads.Internal;

namespace Behlog.Cms.Domain;


public class FileUpload : AggregateRoot<Guid>, IHasMetadata
{

    private FileUpload() { }

    #region props
    public Guid WebsiteId { get; protected set; }
    public string? Title { get; protected set; }
    public string FilePath { get; protected set; }
    public string FileName { get; protected set; }
    public string FileUrl { get; protected set; }
    public string? AlternateFilePath { get; protected set; }
    public string? Extension { get; protected set; }
    public long FileSize { get; protected set; }
    public string? AltTitle { get; protected set; }
    public string? Url { get; protected set; }
    public FileUploadStatus Status { get; protected set; }
    public FileTypeEnum FileType { get; protected set; }
    public DateTime? LastStatusChangedOn { get; protected set; }
    public string? Description { get; protected set; }
    public DateTime CreatedDate { get; protected set; }
    public DateTime? LastUpdated { get; protected set; }
    public string? CreatedByUserId { get; protected set; }
    public string? LastUpdatedByUserId { get; protected set; }
    public string? CreatedByIp { get; protected set; }
    public string? LastUpdatedByIp { get; protected set; }
    #endregion

    #region Navigations
    public Website Website { get; protected set; }

    #endregion

    #region Builders


    public static FileUpload Create(
        CreateFileUploadCommand command, FileUploaderResult uploaderResult,
        FileUploaderResult? alternateFileUploadResult, ISystemDateTime dateTime,
        IBehlogApplicationContext appContext, IIdyfaUserContext userContext)
    {
        command.ThrowExceptionIfArgumentIsNull(nameof(command));
        uploaderResult.ThrowExceptionIfArgumentIsNull(nameof(uploaderResult));
        dateTime.ThrowExceptionIfArgumentIsNull(nameof(dateTime));
        appContext.ThrowExceptionIfArgumentIsNull(nameof(appContext));
        userContext.ThrowExceptionIfArgumentIsNull(nameof(userContext));

        var file = new FileUpload
        {
            Id = Guid.NewGuid(),
            Title = command.Title?.Trim().CorrectYeKe()!,
            Status = FileUploadStatus.Created,
            AltTitle = command.AltTitle?.Trim().CorrectYeKe()!,
            Description = command.Description?.CorrectYeKe()!,
            CreatedDate = dateTime.UtcNow,
            FileName = uploaderResult.FileName,
            FileUrl = uploaderResult.FileUrl,
            Extension = uploaderResult.Extension,
            FileSize = uploaderResult.FileSize,
            WebsiteId = command.WebsiteId,
            FilePath = uploaderResult.FilePath,
            CreatedByIp = appContext.IpAddress, 
            CreatedByUserId = userContext.UserId
        };

        if (alternateFileUploadResult is not null)
        {
            file.AlternateFilePath = alternateFileUploadResult.FilePath;
        }

        file.AddCreatedEvent();
        return file;
    }


    public static FileUpload Create(
        CreateFileWithUrlCommand command, IBehlogApplicationContext appContext, 
        IIdyfaUserContext userContext, ISystemDateTime dateTime)
    {
        command.ThrowExceptionIfArgumentIsNull(nameof(command));
        appContext.ThrowExceptionIfArgumentIsNull(nameof(appContext));
        userContext.ThrowExceptionIfArgumentIsNull(nameof(userContext));
        dateTime.ThrowExceptionIfArgumentIsNull(nameof(dateTime));

        var file = new FileUpload
        {
            Id = Guid.NewGuid(),
            Url = command.Url,
            FileUrl = command.Url,
            Description = command.Description,
            WebsiteId = command.WebsiteId,
            FileType = command.FileType,
            Status = FileUploadStatus.Created,
            Title = command.Title,
            AltTitle = command.AltTitle,
            CreatedDate = dateTime.UtcNow,
            CreatedByUserId = userContext.UserId,
            CreatedByIp = appContext.IpAddress,
        };
        
        file.AddCreatedEvent();
        return file;
    }
    

    public void Update(UpdateFileUploadCommand command, IIdyfaUserContext userContext,
        IBehlogApplicationContext appContext, ISystemDateTime dateTime)
    {
        command.ThrowExceptionIfArgumentIsNull(nameof(command));
        userContext.ThrowExceptionIfArgumentIsNull(nameof(userContext));
        appContext.ThrowExceptionIfArgumentIsNull(nameof(appContext));
        dateTime.ThrowExceptionIfArgumentIsNull(nameof(dateTime));
        
        Title = command.Title;
        AltTitle = command.AltTitle;
        Description = command.Description;
        LastUpdatedByIp = appContext.IpAddress;
        LastUpdatedByUserId = userContext.UserId;
        LastUpdated = dateTime.UtcNow;
        Url = command.Url;

        if (command.Hidden && Status != FileUploadStatus.Hidden)
        {
            ChangeStatus(FileUploadStatus.Hidden, appContext, userContext, dateTime);
        }
        
        AddUpdatedEvent();
    }

    public void SoftDelete(
        IIdyfaUserContext userContext, 
        IBehlogApplicationContext appContext, 
        ISystemDateTime dateTime)
    {
        ChangeStatus(FileUploadStatus.Deleted, appContext, userContext, dateTime);
        AddSoftDeletedEvent();
    }

    public void Archive(
        IIdyfaUserContext userContext, 
        IBehlogApplicationContext appContext, 
        ISystemDateTime dateTime)
    {
        ChangeStatus(FileUploadStatus.Archived, appContext, userContext, dateTime);
        AddArchivedEvent();
    }

    public void Attach(Guid contentId)
    {
        throw new NotImplementedException();
        AddAttachedEvent(contentId);
    }

    public void Remove()
    {
        //TODO : check can be removed
        AddRemovedEvent();
    }
    

    #endregion

    #region Events
    
    private void AddCreatedEvent()
    {
        var e = new FileUploadCreatedEvent(
            Id, Title, FilePath, FileName, FileUrl, AlternateFilePath, Extension, FileSize, AltTitle,
            Url, Status, Description, CreatedDate, CreatedByUserId, CreatedByIp);
        Enqueue(e);
    }


    private void AddUpdatedEvent()
    {
        var e = new FileUpdatedEvent(
            Id, Title, FilePath, FileName, AlternateFilePath, Extension, FileSize, AltTitle,
            Url, Status, Description, CreatedByUserId, LastUpdatedByUserId,
            CreatedByIp, LastUpdatedByIp, LastStatusChangedOn, CreatedDate);
        Enqueue(e);
    }

    private void AddArchivedEvent()
    {
        var e = new FileUploadArchivedEvent(Id, FileName, FilePath);
        Enqueue(e);
    }

    private void AddAttachedEvent(Guid contentId)
    {
        var e = new FileUploadAttachedEvent(Id, contentId, FileName, FilePath);
        Enqueue(e);
    }

    private void AddSoftDeletedEvent()
    {
        var e = new FileUploadSoftDeletedEvent(Id);
        Enqueue(e);
    }

    private void AddRemovedEvent()
    {
        var e = new FileUploadRemovedEvent(Id, FileName, FilePath);
        Enqueue(e);
    }

    #endregion

    #region helpers

    private void ChangeStatus(
        FileUploadStatus status, IBehlogApplicationContext appContext, 
        IIdyfaUserContext userContext, ISystemDateTime dateTime)
    {
        appContext.ThrowExceptionIfArgumentIsNull(nameof(appContext));
        userContext.ThrowExceptionIfArgumentIsNull(nameof(userContext));
        dateTime.ThrowExceptionIfArgumentIsNull(nameof(dateTime));
        
        Status = status;
        LastStatusChangedOn = dateTime.UtcNow;
        LastUpdatedByUserId = userContext.UserId;
        LastUpdatedByIp = appContext.IpAddress;
    }

    #endregion
    
}