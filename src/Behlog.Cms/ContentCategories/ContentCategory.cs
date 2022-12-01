using System.ComponentModel.DataAnnotations.Schema;
using Behlog.Cms.Commands;
using Behlog.Core;
using Behlog.Extensions;
using Behlog.Cms.Events;
using Behlog.Core.Contracts;
using Behlog.Core.Domain;
using Idyfa.Core;
using Idyfa.Core.Contracts;

namespace Behlog.Cms.Domain;

public partial class ContentCategory : AggregateRoot<Guid>, IHasMetadata 
{

    private ContentCategory()
    {
    }
    

    #region Methods

    public static ContentCategory Create(
        CreateContentCategoryCommand command, IIdyfaUserContext userContext, 
        IBehlogApplicationContext appContext, ISystemDateTime dateTime)
    {
        command.ThrowExceptionIfArgumentIsNull(nameof(command));
        userContext.ThrowExceptionIfArgumentIsNull(nameof(userContext));
        appContext.ThrowExceptionIfArgumentIsNull(nameof(appContext));
        
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
            CreatedDate = dateTime.UtcNow,
            CreatedByIp = appContext.IpAddress,
            WebsiteId = command.WebsiteId, //TODO : read from User claims
            CreatedByUserId = userContext.UserId
        };
        
        category.AddCreatedEvent();
        return category;
    }

    public void Update(
        UpdateContentCategoryCommand command, IIdyfaUserContext userContext, 
        IBehlogApplicationContext appContext, ISystemDateTime dateTime) 
    {
        command.ThrowExceptionIfArgumentIsNull(nameof(command));
        userContext.ThrowExceptionIfArgumentIsNull(nameof(userContext));
        appContext.ThrowExceptionIfArgumentIsNull(nameof(appContext));
        dateTime.ThrowExceptionIfArgumentIsNull(nameof(dateTime));
        
        checkRequiredFields(command);
        
        Title = command.Title?.Trim().CorrectYeKe()!;
        AltTitle = command.AltTitle?.Trim().CorrectYeKe()!;
        Slug = command.Slug?.Trim().CorrectYeKe().MakeSlug()!;
        LangId = command.LangId;
        ParentId = command.ParentId;
        Description = command.Description?.CorrectYeKe()!;
        
        ChangeStatus(
            command.Enabled ? EntityStatus.Enabled : EntityStatus.Disabled, 
            userContext, appContext, dateTime);
        
        LastUpdatedByIp = appContext.IpAddress;
        LastUpdatedByUserId = userContext.UserId;
        LastUpdated = dateTime.UtcNow;
        
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

    private void ChangeStatus(
        EntityStatus status, IIdyfaUserContext userContext, 
        IBehlogApplicationContext appContext, ISystemDateTime dateTime)
    {
        if (this.Status == status) return;
        
        LastStatusChangedOn = dateTime.UtcNow;
        Status = status;
        LastUpdatedByIp = appContext.IpAddress;
        LastUpdatedByUserId = userContext.UserId;
    }
    
    #endregion

    #region Props
    public Guid? WebsiteId { get; protected set; }
    public string Title { get; protected set; }
    public string? AltTitle { get; protected set; }
    public string Slug { get; protected set; }
    public Guid LangId { get; protected set; }
    public Guid? ParentId { get; protected set; }
    public string? Description { get; protected set; }
    public Guid? ContentTypeId { get; protected set; }
    public EntityStatus Status { get; protected set; }
    public DateTime CreatedDate { get; protected set; }
    public DateTime? LastUpdated { get; protected set; }
    
    public string? CreatedByUserId { get; protected set; }
    public string? LastUpdatedByUserId { get; protected set; }
    public string? CreatedByIp { get; protected set; }
    public string? LastUpdatedByIp { get; protected set; }
    public DateTime? LastStatusChangedOn { get; protected set; }

    #endregion

    #region Navigations

    public Language Language { get; protected set; }
    
    public ContentType ContentType { get; protected set; }
    
    public Website Website { get; protected set; }
    
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