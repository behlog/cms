using Behlog.Cms.Commands;
using Behlog.Cms.Events;
using Behlog.Core.Domain;
using Behlog.Extensions;

namespace Behlog.Core;

public class ContentType : BehlogEntity<Guid> {
    
    private ContentType()
    {
        
    }

    #region Props
    
    public string SystemName { get; protected set; }
    public string Title { get; protected set; }
    public string Slug { get; protected set; }
    public string Description { get; protected set; }
    public string Lang { get; protected set; }
    public EntityStatus Status { get; protected set; }
    public DateTime CreateDate { get; protected set; }
    public DateTime? ModifyDate { get; protected set; }
    public DateTime? LastStatusChangedOn { get; protected set; }

    #endregion


    #region Builders

    public static async Task<ContentType> CreateAsync(
        CreateContentTypeCommand command, IBehlogManager manager)
    {
        command.ThrowExceptionIfArgumentIsNull(nameof(command));
        manager.ThrowExceptionIfArgumentIsNull(nameof(manager));

        var contentType = new ContentType
        {
            Id = Guid.NewGuid(),
            SystemName = command.SystemName.Trim(),
            Lang = command.Lang,
            Description = command.Description?.CorrectYeKe()!,
            Slug = command.Slug?.Trim().CorrectYeKe()!,
            Status = EntityStatus.Enabled,
            Title = command.Title?.Trim().CorrectYeKe()!,
            CreateDate = DateTime.UtcNow
        };

        await contentType.PublishCreatedEvent(manager);

        return await Task.FromResult(contentType);
    }


    public async Task UpdateAsync(
        UpdateContentTypeCommand command, IBehlogManager manager)
    {
        command.ThrowExceptionIfArgumentIsNull(nameof(command));
        manager.ThrowExceptionIfArgumentIsNull(nameof(manager));

        Title = command.Title?.Trim().CorrectYeKe()!;
        Slug = command.Slug?.Trim().MakeSlug().CorrectYeKe()!;
        SystemName = command.SystemName?.Trim()!;
        Lang = command.Lang;
        if (command.Enabled && Status != EntityStatus.Enabled ||
            !command.Enabled && Status != EntityStatus.Disabled)
        {
            LastStatusChangedOn = DateTime.UtcNow;
        }
        Status = command.Enabled ? EntityStatus.Enabled : EntityStatus.Disabled;
        Description = command.Description?.CorrectYeKe()!;
        ModifyDate = DateTime.UtcNow;

        await PublishUpdatedEvent(manager);
    }


    public async Task SoftDeleteAsync(IBehlogManager manager)
    {
        manager.ThrowExceptionIfArgumentIsNull(nameof(manager));
        
        Status = EntityStatus.Deleted;
        LastStatusChangedOn = DateTime.UtcNow;

        var e = new ContentTypeSoftDeletedEvent(Id);
        await manager.PublishAsync(e).ConfigureAwait(false);
    }

    #endregion

    #region Events

    protected async Task PublishCreatedEvent(IBehlogManager manager)
    {
        var e = new ContentTypeCreatedEvent(
            Id, SystemName, Title, Lang, Slug, Description);
        await manager.PublishAsync(e).ConfigureAwait(false);
    }
    
    protected async Task PublishUpdatedEvent(IBehlogManager manager)
    {
        var e = new ContentTypeUpdatedEvent(
            Id, SystemName, Title, Lang, Slug, Status,
            Description, LastStatusChangedOn);
        await manager.PublishAsync(e).ConfigureAwait(false);
    }

    #endregion
}