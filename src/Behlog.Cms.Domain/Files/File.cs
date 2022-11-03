using Behlog.Cms.Commands;
using Behlog.Cms.Events;
using Behlog.Core;
using Behlog.Core.Domain;
using Behlog.Extensions;

namespace Behlog.Cms.Domain;

public class File : BehlogEntity<Guid>, IHasMetadata
{

    private File() { }

    #region props
    
    public string Title { get; protected set; }
    public string FilePath { get; protected set; }
    public string AlternateFilePath { get; protected set; }
    public string Extension { get; protected set; }
    public string AltTitle { get; protected set; }
    public string Url { get; protected set; }
    public FileStatus Status { get; protected set; }
    public DateTime? LastStatusChangedOn { get; protected set; }
    public string Description { get; protected set; }
    public DateTime CreatedDate { get; protected set; }
    public DateTime? LastUpdated { get; protected set; }
    public string CreatedByUserId { get; protected set; }
    public string LastUpdatedByUserId { get; protected set; }
    public string CreatedByIp { get; protected set; }
    public string LastUpdatedByIp { get; protected set; }
    #endregion

    #region Builders
    
    
    public static async Task<File> CreateAsync(
        CreateFileCommand command, IBehlogManager manager)
    {
        command.ThrowExceptionIfArgumentIsNull(nameof(command));
        manager.ThrowExceptionIfArgumentIsNull(nameof(manager));

        var file = new File
        {
            Id = Guid.NewGuid(),
            Title = command.Title?.Trim().CorrectYeKe()!,
            Status = FileStatus.Created,
            AltTitle = command.AltTitle?.Trim().CorrectYeKe()!,
            Description = command.Description?.CorrectYeKe()!,
            CreatedDate = DateTime.UtcNow,
            AlternateFilePath = command.AlternateFilePath,
            CreatedByIp = "", //TODO : HttpContext
            CreatedByUserId = "", //TODO : from UserContext
        };

        await file.PublishCreatedEvent(manager);
        return await Task.FromResult(file);
    }


    public async Task UpdateAsync(
        UpdateFileCommand command, IBehlogManager manager)
    {
        command.ThrowExceptionIfArgumentIsNull(nameof(command));
        manager.ThrowExceptionIfArgumentIsNull(nameof(manager));

        Title = command.Title;
        AltTitle = command.AltTitle;
        Description = command.Description;

        if (command.Hidden && Status != FileStatus.Hidden)
        {
            ChangeStatus(FileStatus.Hidden);
        }
        

        await PublishUpdatedEvent(manager);
    }

    #endregion

    #region Event publisher

    private async Task PublishCreatedEvent(IBehlogManager manager)
    {
        var e = new FileCreatedEvent(
            Id, Title, FilePath, AlternateFilePath, Extension, AltTitle,
            Url, Status, Description, CreatedDate, CreatedByUserId, CreatedByIp);
        await manager.PublishAsync(e).ConfigureAwait(false);
    }

    private async Task PublishUpdatedEvent(IBehlogManager manager)
    {
        var e = new FileUpdatedEvent(
            Id, Title, FilePath, AlternateFilePath, Extension, AltTitle,
            Url, Status, Description, CreatedByUserId, LastUpdatedByUserId,
            CreatedByIp, LastUpdatedByIp, LastStatusChangedOn, CreatedDate);
        await manager.PublishAsync(e).ConfigureAwait(false);
    }

    #endregion

    #region helpers

    private void ChangeStatus(FileStatus status)
    {
        Status = status;
        LastStatusChangedOn = DateTime.UtcNow;
        LastUpdatedByUserId = ""; //TODO : from userContext
        LastUpdatedByIp = ""; //TODO : from HttpContext
    }

    #endregion
    
}