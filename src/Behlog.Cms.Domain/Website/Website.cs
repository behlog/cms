using Behlog.Cms.Commands;
using Behlog.Cms.Contracts;
using Behlog.Cms.Events;
using Behlog.Cms.Models;
using Behlog.Core;
using Behlog.Core.Contracts;
using Behlog.Core.Domain;
using Behlog.Extensions;
using Idyfa.Core.Contracts;

namespace Behlog.Cms.Domain;

public partial class Website : AggregateRoot<Guid>
{
    
    private Website() { }


    #region props

    public string Name { get; protected set; }
    public string Title { get; protected set; }
    public string? Description { get; protected set; }
    public string? Keywords { get; protected set; }
    public string? Url { get; protected set; }
    public string OwnerUserId { get; protected set; }
    public Guid? DefaultLangId { get; protected set; }
    public WebsiteStatus Status { get; protected set; }
    public DateTime CreatedDate { get; protected set; }
    public string? Password { get; protected set; }
    public bool IsReadOnly { get; protected set; }
    public string? Email { get; protected set; }
    public string? CopyrightText { get; protected set; }
    public DateTime? LastUpdated { get; protected set; }
    public DateTime? LastStatusChangedOn { get; protected set; }
    public string? LastUpdatedByUserId { get; protected set; }
    public string? LastUpdatedByIp { get; protected set; }
    
    #endregion

    #region Navigations

    public ICollection<WebsiteMeta> Meta { get; protected set; }

    #endregion

    #region Builders


    public static async Task<Website> CreateAsync(
        CreateWebsiteCommand command, IWebsiteService service)
    {
        command.ThrowExceptionIfArgumentIsNull(nameof(command));
        service.ThrowExceptionIfArgumentIsNull(nameof(service));

        await CheckNameExistAsync(null, command.Name, service);
        
        var website = new Website
        {
            Id = Guid.NewGuid(),
            Name = command.Name?.Trim().CorrectYeKe()!,
            Email = command.Email?.Trim(),
            Title = command.Title?.Trim().CorrectYeKe()!,
            Password = command.Password,
            Status = WebsiteStatus.Online,
            Description = command.Description,
            Keywords = command.Keywords,
            Url = command.Url,
            CopyrightText = command.CopyrightText,
            CreatedDate = DateTime.UtcNow,
            DefaultLangId = command.DefaultLangId,
            IsReadOnly = command.IsReadOnly,
            OwnerUserId = "", //TODO : Set UserId
        };
        
        var createdEvent = website.GetCreatedEvent();
        website.Enqueue(createdEvent);
        
        return website;
    }


    public async Task UpdateAsync(
        UpdateWebsiteCommand command, 
        IWebsiteService service,
        IIdyfaUserContext userContext, 
        IBehlogApplicationContext applicationContext)
    {
        command.ThrowExceptionIfArgumentIsNull(nameof(command));
        userContext.ThrowExceptionIfArgumentIsNull(nameof(userContext));
        applicationContext.ThrowExceptionIfArgumentIsNull(nameof(applicationContext));
        service.ThrowExceptionIfArgumentIsNull(nameof(service));
        
        await CheckNameExistAsync(Id, command.Name, service);
        
        Title = command.Title?.Trim().CorrectYeKe()!;
        Name = command.Name?.Trim().CorrectYeKe()!;
        Email = command.Email?.Trim();
        Password = command.Password;
        Description = command.Description?.CorrectYeKe();
        Url = command.Url;
        CopyrightText = command.CopyrightText?.Trim().CorrectYeKe();
        DefaultLangId = command.DefaultLangId;
        Keywords = command.Keywords?.CorrectYeKe();
        LastUpdated = DateTime.UtcNow;
        LastUpdatedByIp = applicationContext.IpAddress;

        

        var updatedEvent = this.GetUpdatedEvent();
        Enqueue(updatedEvent);
    }


    public void SoftDelete(
        IIdyfaUserContext userContext, IBehlogApplicationContext applicationContext)
    {
        //TODO : check if can softdelete
        ChangeStatus(WebsiteStatus.Deleted, userContext.UserId, applicationContext.IpAddress);
        var e = new WebsiteSoftDeletedEvent(Id);
        Enqueue(e);
    }


    public void SetStatus(
        WebsiteStatus status, IIdyfaUserContext userContext, 
        IBehlogApplicationContext applicationContext)
    {
        if (Equals(status, WebsiteStatus.Deleted))
            throw new BehlogException("Use SoftDeleteWebsiteCommand instead.");
        
        ChangeStatus(status, userContext.UserId, applicationContext.IpAddress);
    }


    public void Remove()
    {
        var e = new WebsiteRemovedEvent(Id);
        Enqueue(e);
    }
    
    private void ChangeStatus(
        WebsiteStatus status, string? userId, string? ipAddress = null)
    {
        if (Equals(Status, status))
            return;

        //TODO : change To InvalidStatusException
        if (Equals(Status, WebsiteStatus.Deleted))
            throw new BehlogException("Cannot change status of a Deleted Website");

        Status = status;
        LastStatusChangedOn = DateTime.UtcNow;
        LastUpdatedByUserId = userId;
        LastUpdatedByIp = ipAddress;
    }
    
    #endregion
    
    
}