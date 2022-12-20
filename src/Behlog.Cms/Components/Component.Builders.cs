using Behlog.Core;
using Behlog.Extensions;
using Behlog.Cms.Events;
using Behlog.Cms.Models;
using Behlog.Cms.Commands;
using Behlog.Cms.Contracts;
using Idyfa.Core.Contracts;
using Behlog.Core.Contracts;

namespace Behlog.Cms.Domain;


public partial class Component
{

    public static async Task<Component> CreateAsync(
        CreateComponentCommand command, IBehlogApplicationContext appContext,
        IIdyfaUserContext userContext, ISystemDateTime dateTime, IComponentService service)
    {
        command.ThrowExceptionIfArgumentIsNull(nameof(command));
        appContext.ThrowExceptionIfArgumentIsNull(nameof(appContext));
        userContext.ThrowExceptionIfArgumentIsNull(nameof(userContext));
        dateTime.ThrowExceptionIfArgumentIsNull(nameof(dateTime));
        service.ThrowExceptionIfArgumentIsNull(nameof(service));

        var component = new Component
        {
            Id = Guid.NewGuid(),
            Attributes = command.Attributes,
            Author = command.Author,
            Category = command.Category,
            Description = command.Description,
            Keywords = command.Keywords,
            LangId = command.LangId,
            Name = command.Name,
            Status = EntityStatus.Enabled,
            Title = command.Title?.CorrectYeKe().Trim()!,
            AuthorEmail = command.AuthorEmail?.Trim(),
            ComponentType = command.ComponentType.Trim(),
            CreatedDate = dateTime.UtcNow,
            IsRtl = command.IsRtl,
            ParentId = command.ParentId,
            ViewPath = command.ViewPath,
            WebsiteId = command.WebsiteId,
            CreatedByIp = appContext.IpAddress,
            CreatedByUserId = userContext.UserId
        };
        
        component.Meta = command.Meta.Convert(component.Id);
        
        component.AddCreatedEvent();
        
        return await Task.FromResult(component);
    }
    
    
    public async Task UpdateAsync(
        UpdateComponentCommand command, IBehlogApplicationContext appContext,
        IIdyfaUserContext userContext, ISystemDateTime dateTime, IComponentService service)
    {
        command.ThrowExceptionIfArgumentIsNull(nameof(command));
        appContext.ThrowExceptionIfArgumentIsNull(nameof(appContext));
        userContext.ThrowExceptionIfArgumentIsNull(nameof(userContext));
        dateTime.ThrowExceptionIfArgumentIsNull(nameof(dateTime));
        service.ThrowExceptionIfArgumentIsNull(nameof(service));

        LangId = command.LangId;
        Name = command.Name;
        Title = command.Title;
        ComponentType = command.ComponentType;
        Category = command.Category;
        Attributes = command.Attributes;
        Description = command.Description;
        Author = command.Author;
        AuthorEmail = command.AuthorEmail;
        ParentId = command.ParentId;
        Status = command.Enabled ? EntityStatus.Enabled : EntityStatus.Disabled;
        IsRtl = command.IsRtl;
        Keywords = command.Keywords;
        ViewPath = command.ViewPath;
        Meta = command.Meta?.Convert(Id)!;
        Files = command.Files?.Convert(Id)!;
        
        AddUpdatedEvent();
    }

    /// <summary>
    /// Mark a <see cref="Component"/> as <see cref="EntityStatus.Deleted"/>.
    /// When soft deleted, the Component wont be displayed anymore across website.
    /// but it can be recycled.
    /// </summary>
    public void SoftDelete(
        IIdyfaUserContext userContext, IBehlogApplicationContext appContext, ISystemDateTime dateTime)
    {
        userContext.ThrowExceptionIfArgumentIsNull(nameof(userContext));
        appContext.ThrowExceptionIfArgumentIsNull(nameof(appContext));
        dateTime.ThrowExceptionIfArgumentIsNull(nameof(dateTime));
        
        Status = EntityStatus.Deleted;
        LastUpdated = dateTime.UtcNow;
        LastUpdatedByIp = appContext.IpAddress;
        LastUpdatedByUserId = userContext.UserId;
        
        AddSoftDeletedEvent();
    }


    public void Remove()
    {
        AddRemovedEvent();
    }
     
    #region events


    private void AddCreatedEvent()
    {
        var e = new ComponentCreatedEvent(
            Id, WebsiteId, LangId, Name, Title, ComponentType, Category, Description, Attributes,
            Status, Author, AuthorEmail, ParentId, IsRtl, Keywords, ViewPath, CreatedDate, CreatedByUserId,
            CreatedByIp, Meta?.Select(_ => _.ToResult()).ToList()!);
        
        Enqueue(e);
    }
    
    private void AddUpdatedEvent()
    {
        var e = new ComponentUpdatedEvent(
            Id, WebsiteId, LangId, Name, Title, ComponentType, Category, Description, Attributes,
            Status, Author, AuthorEmail, ParentId, IsRtl, Keywords, ViewPath, CreatedDate, LastUpdated,
            CreatedByUserId, CreatedByIp, LastUpdatedByUserId, LastUpdatedByIp,
            Meta?.Select(_ => _.ToResult()).ToList()!);
        
        Enqueue(e);
    }

    private void AddSoftDeletedEvent()
    {
        var e = new ComponentSoftDeletedEvent(Id);
        Enqueue(e);
    }

    private void AddRemovedEvent()
    {
        var e = new ComponentRemovedEvent(Id);
        Enqueue(e);
    }

    #endregion
}