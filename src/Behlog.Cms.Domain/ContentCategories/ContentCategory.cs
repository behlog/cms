using Behlog.Cms.Commands;
using Behlog.Core;
using Behlog.Extensions;
using Behlog.Cms.Events;
using Behlog.Core.Domain;

namespace Behlog.Cms.Domain;

public partial class ContentCategory : BehlogEntity<Guid> 
{

    private ContentCategory()
    {
    }
    

    #region Methods

    public static async Task<ContentCategory> CreateAsync(
        CreateContentCategoryCommand command, IBehlogManager manager)
    {
        manager.ThrowExceptionIfArgumentIsNull(nameof(manager));
        checkRequiredFields(command);
        
        var category = new ContentCategory
        {
            Id = Guid.NewGuid(),
            Title = command.Title?.Trim().CorrectYeKe()!,
            Slug = command.Slug?.Trim().CorrectYeKe().MakeSlug()!,
            Status = EntityStatus.Enabled,
            AltTitle = command.AltTitle?.Trim().CorrectYeKe()!,
            ParentId = command.ParentId,
            ContentTypeId = command.ContentTypeId,
            Description = command.Description,
            CreateDate = DateTime.UtcNow
        };
        await category.PublishCreatedEvent(manager);
        return await Task.FromResult(category);
    }

    public async Task UpdateAsync(
        UpdateContentCategoryCommand command, IBehlogManager manager) {
        manager.ThrowExceptionIfArgumentIsNull(nameof(manager));
        checkRequiredFields(command);
        
        Title = command.Title?.Trim().CorrectYeKe()!;
        Slug = command.Slug?.Trim().CorrectYeKe().MakeSlug()!;
        ParentId = command.ParentId;
        Description = command.Description?.CorrectYeKe()!;
        ContentTypeId = command.ContentTypeId;
        ModifyDate = DateTime.UtcNow;
        if (Status == EntityStatus.Enabled && !command.Enabled ||
            Status != EntityStatus.Enabled && command.Enabled)
        {
            LastStatusChangedOn = DateTime.UtcNow;
        }
        Status = command.Enabled ? EntityStatus.Enabled : EntityStatus.Disabled;

        await PublishUpdatedEvent(manager);
    }

    public async Task SoftDeleteAsync(IBehlogManager manager)
    {
        Status = EntityStatus.Deleted;
        LastStatusChangedOn = DateTime.UtcNow;

        var e = new ContentCategorySoftDeletedEvent(Id);
        await manager.PublishAsync(e).ConfigureAwait(false);
    }

    #endregion

    #region Props
    public string Title { get; protected set; }
    public string AltTitle { get; protected set; }
    public string Slug { get; protected set; }
    public Guid? ParentId { get; protected set; }
    public string Description { get; protected set; }
    public Guid? ContentTypeId { get; protected set; }
    public EntityStatus Status { get; protected set; }
    public DateTime CreateDate { get; protected set; }
    public DateTime? ModifyDate { get; protected set; }
    public DateTime? LastStatusChangedOn { get; protected set; }

    #endregion

    #region Events

    protected async Task PublishCreatedEvent(IBehlogManager manager)
    {
        var e = new ContentCategoryCreatedEvent(
            id: Id,
            title: Title,
            altTitle: AltTitle,
            slug: Slug,
            parentId: ParentId,
            description: Description,
            contentTypeId: ContentTypeId,
            status: Status
        );
        await manager.PublishAsync(e).ConfigureAwait(false);
    }

    protected async Task PublishUpdatedEvent(IBehlogManager manager) 
    {
        var e = new ContentCategoryUpdatedEvent(
            id: Id,
            title: Title,
            altTitle: AltTitle,
            slug: Slug,
            parentId: ParentId,
            description: Description,
            contentTypeId: ContentTypeId,
            status: Status
        );
        await manager.PublishAsync(e).ConfigureAwait(false);
    }

    #endregion
}