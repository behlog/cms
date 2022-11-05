using System.ComponentModel.DataAnnotations.Schema;
using Behlog.Cms.Commands;
using Behlog.Core;
using Behlog.Extensions;
using Behlog.Cms.Events;
using Behlog.Core.Domain;

namespace Behlog.Cms.Domain;

public partial class ContentCategory : AggregateRoot<Guid> 
{

    private ContentCategory()
    {
    }
    

    #region Methods

    public static ContentCategory Create(CreateContentCategoryCommand command)
    {
        command.ThrowExceptionIfArgumentIsNull(nameof(command));
        checkRequiredFields(command);
        
        var category = new ContentCategory
        {
            Id = Guid.NewGuid(),
            Title = command.Title?.Trim().CorrectYeKe()!,
            Slug = command.Slug?.Trim().CorrectYeKe().MakeSlug()!,
            LangId = command.LangId,
            Status = EntityStatus.Enabled,
            AltTitle = command.AltTitle?.Trim().CorrectYeKe()!,
            ParentId = command.ParentId,
            ContentTypeId = command.ContentTypeId,
            Description = command.Description,
            CreatedDate = DateTime.UtcNow
        };
        
        category.AddCreatedEvent();
        return category;
    }

    public void Update(UpdateContentCategoryCommand command) {
        command.ThrowExceptionIfArgumentIsNull(nameof(command));
        checkRequiredFields(command);
        
        Title = command.Title?.Trim().CorrectYeKe()!;
        AltTitle = command.AltTitle?.Trim().CorrectYeKe()!;
        Slug = command.Slug?.Trim().CorrectYeKe().MakeSlug()!;
        LangId = command.LangId;
        ParentId = command.ParentId;
        Description = command.Description?.CorrectYeKe()!;
        ContentTypeId = command.ContentTypeId;
        LastUpdated = DateTime.UtcNow;
        if (Status == EntityStatus.Enabled && !command.Enabled ||
            Status != EntityStatus.Enabled && command.Enabled)
        {
            LastStatusChangedOn = DateTime.UtcNow;
        }
        Status = command.Enabled ? EntityStatus.Enabled : EntityStatus.Disabled;

        AddUpdatedEvent();
    }

    public void SoftDelete()
    {
        Status = EntityStatus.Deleted;
        LastStatusChangedOn = DateTime.UtcNow;

        AddSoftDeletedEvent();
    }


    public void Remove()
    {
        //TODO : check if can be removed
        AddRemovedEvent();
    }

    #endregion

    #region Props
    public string Title { get; protected set; }
    public string AltTitle { get; protected set; }
    public string Slug { get; protected set; }
    public Guid LangId { get; protected set; }
    public Guid? ParentId { get; protected set; }
    public string Description { get; protected set; }
    public Guid? ContentTypeId { get; protected set; }
    public EntityStatus Status { get; protected set; }
    public DateTime CreatedDate { get; protected set; }
    public DateTime? LastUpdated { get; protected set; }
    public DateTime? LastStatusChangedOn { get; protected set; }

    #endregion

    #region Navigations

    public ICollection<ContentCategoryItem> Contents { get; protected set; } 
        = new HashSet<ContentCategoryItem>();

    #endregion

    #region Events

    private void AddCreatedEvent()
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
        
        Enqueue(e);
    }
    
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


    private void AddUpdatedEvent()
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
        
        Enqueue(e);
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


    private void AddSoftDeletedEvent()
    {
        var e = new ContentCategorySoftDeletedEvent(Id);
        Enqueue(e);
    }

    private void AddRemovedEvent()
    {
        var e = new ContentCategoryRemovedEvent(Id);
        Enqueue(e);
    }
    #endregion
}