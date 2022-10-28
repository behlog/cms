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

        var e = new ContentTypeCreatedEvent(
            contentType.Id, command.SystemName, contentType.Title,
            contentType.Slug, command.Description);
        await manager.PublishAsync(e).ConfigureAwait(false);

        return await Task.FromResult(contentType);
    }

    #endregion
}