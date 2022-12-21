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

        await GuardAgainstDuplicateName(component.Id, component.WebsiteId, component.Name, service);

        if (command.Files.Any())
        {
            command.Files.ToList().ForEach(
                _=> component._files.Add(ComponentFile
                        .New(component.Id, _.FileId)
                        .WithTitle(_.Title)
                        .WithFileName(_.FileName)
                        .WithDescription(_.Description))
                );
        }

        if (command.Meta.Any())
        {
            command.Meta.ToList().ForEach(
                _=> component._meta.Add(ComponentMeta
                        .New(_.MetaKey, _.MetaValue!)
                        .WithCategory(_.Category)
                        .WithTitle(_.Title)
                        .WithDescription(_.Description)
                        .WithLangId(_.LangId)
                        .WithOrderNum(_.OrderNum)
                        .WithOwnerId(component.Id)
                        .WithStatus(_.Enabled ? EntityStatus.Enabled : EntityStatus.Disabled)
                        .Build())
                );
        }
        
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

        await GuardAgainstDuplicateName(Id, WebsiteId, command.Name, service);
        
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


    public void AddFiles(
        IReadOnlyCollection<ComponentFileCommand> files, IIdyfaUserContext userContext, 
        IBehlogApplicationContext appContext, ISystemDateTime dateTime)
    {
        userContext.ThrowExceptionIfArgumentIsNull(nameof(userContext));
        appContext.ThrowExceptionIfArgumentIsNull(nameof(appContext));
        dateTime.ThrowExceptionIfArgumentIsNull(nameof(dateTime));

        addFiles(files);
        
        LastUpdated = dateTime.UtcNow;
        LastUpdatedByIp = appContext.IpAddress;
        LastUpdatedByUserId = userContext.UserId;
        //TODO : raise proper event
    }


    public void AddMeta(
        IReadOnlyCollection<MetaCommand> meta, IIdyfaUserContext userContext,
        IBehlogApplicationContext appContext, ISystemDateTime dateTime)
    {
        userContext.ThrowExceptionIfArgumentIsNull(nameof(userContext));
        appContext.ThrowExceptionIfArgumentIsNull(nameof(appContext));
        dateTime.ThrowExceptionIfArgumentIsNull(nameof(dateTime));
        
        addMeta(meta);
        
        LastUpdated = dateTime.UtcNow;
        LastUpdatedByIp = appContext.IpAddress;
        LastUpdatedByUserId = userContext.UserId;
        //TODO : raise proper event
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
    
    private void addFiles(IReadOnlyCollection<ComponentFileCommand> files)
    {
        if (files is null || !files.Any())
            throw new ArgumentNullException(nameof(files));
        
        foreach (var file in files)
        {
            if (!_files.Any(_ => _.FileId == file.FileId))
            {
                _files.Add(ComponentFile
                    .New(Id, file.FileId)
                    .WithTitle(file.Title)
                    .WithFileName(file.FileName)
                    .WithDescription(file.Description));
                continue;
            }
            
            int idx = 0;
            foreach (var _file in _files)
            {
                if (file.FileId == _file.FileId)
                {
                    _files[idx].WithDescription(file.Description);
                    _files[idx].WithTitle(file.Title);
                    _files[idx].WithFileName(file.FileName);
                    break;
                }
                idx++;
            }
        }
    }
    
    private void addMeta(IReadOnlyCollection<MetaCommand> meta)
    {
        if (meta is null || !meta.Any())
            throw new ArgumentNullException(nameof(meta));
        
        foreach (var m in meta)
        {
            if (!_meta.Any(_ => _.MetaKey.ToUpper() == m.MetaKey.ToUpper()))
            {
                _meta.Add(ComponentMeta.New(m.MetaKey, m.MetaValue!)
                    .WithTitle(m.Title!)
                    .WithDescription(m.Description!)
                    .WithCategory(m.Category!)
                    .WithStatus(m.Enabled ? EntityStatus.Enabled : EntityStatus.Disabled)
                    .WithLangId(m.LangId)
                    .WithOrderNum(m.OrderNum)
                    .WithOwnerId(Id)
                    .Build());
                continue;
            }
            
            int idx = 0;
            foreach (var _m in _meta)
            {
                if (m.MetaKey.ToUpper() == _m.MetaKey.ToUpper())
                {
                    _meta[idx].WithTitle(m.Title);
                    _meta[idx].WithDescription(m.Description!);
                    _meta[idx].WithCategory(m.Category!);
                    _meta[idx].WithStatus(m.Enabled ? EntityStatus.Enabled : EntityStatus.Disabled);
                    _meta[idx].WithLangId(m.LangId);
                    _meta[idx].WithValue(m.MetaValue!);
                    _meta[idx].WithOrderNum(m.OrderNum);
                    
                    break;
                }
                idx++;
            }
        }
    }
    
    #endregion
}