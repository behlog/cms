using Behlog.Cms.Commands;
using Behlog.Cms.Events;
using Behlog.Core;
using Behlog.Core.Domain;
using Behlog.Extensions;

namespace Behlog.Cms.Domain;

public class FileUpload : AggregateRoot<Guid>, IHasMetadata
{

    private FileUpload() { }

    #region props
    public Guid WebsiteId { get; protected set; }
    public string? Title { get; protected set; }
    public string FilePath { get; protected set; }
    public string FileName { get; protected set; }
    public string? AlternateFilePath { get; protected set; }
    public string? Extension { get; protected set; }
    public long FileSize { get; protected set; }
    public string? AltTitle { get; protected set; }
    public string? Url { get; protected set; }
    public FileUploadStatus Status { get; protected set; }
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


    public static FileUpload Create(CreateFileUploadCommand command)
    {
        command.ThrowExceptionIfArgumentIsNull(nameof(command));

        var file = new FileUpload
        {
            Id = Guid.NewGuid(),
            Title = command.Title?.Trim().CorrectYeKe()!,
            Status = FileUploadStatus.Created,
            AltTitle = command.AltTitle?.Trim().CorrectYeKe()!,
            Description = command.Description?.CorrectYeKe()!,
            CreatedDate = DateTime.UtcNow,
            AlternateFilePath = command.AlternateFilePath,
            CreatedByIp = "", //TODO : HttpContext
            CreatedByUserId = "", //TODO : from UserContext
        };

        file.AddCreatedEvent();
        return file;
    }


    public void Update(UpdateFileUploadCommand command)
    {
        command.ThrowExceptionIfArgumentIsNull(nameof(command));

        Title = command.Title;
        AltTitle = command.AltTitle;
        Description = command.Description;

        if (command.Hidden && Status != FileUploadStatus.Hidden)
        {
            ChangeStatus(FileUploadStatus.Hidden);
        }
        
        AddUpdatedEvent();
    }

    public void SoftDelete()
    {
        ChangeStatus(FileUploadStatus.Deleted);
        AddSoftDeletedEvent();
    }

    public void Archive()
    {
        ChangeStatus(FileUploadStatus.Archived);
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
            Id, Title, FilePath, FileName, AlternateFilePath, Extension, FileSize, AltTitle,
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

    private void ChangeStatus(FileUploadStatus status)
    {
        Status = status;
        LastStatusChangedOn = DateTime.UtcNow;
        LastUpdatedByUserId = ""; //TODO : from userContext
        LastUpdatedByIp = ""; //TODO : from HttpContext
    }

    #endregion
    
}