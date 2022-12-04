using Behlog.Cms.Commands;
using Behlog.Cms.Events;
using Behlog.Cms.Models;
using Behlog.Core;
using Behlog.Core.Contracts;
using Behlog.Core.Domain;
using Behlog.Extensions;
using Idyfa.Core.Contracts;

namespace Behlog.Cms.Domain;


public class Block : AggregateRoot<Guid>, IHasMetadata
{
    private Block() { }


    #region props

    public string Name { get; protected set; }
    public string Title { get; protected set; }
    public string BlockType { get; protected set; }
    public string? Category { get; protected set; }
    public string? Author { get; protected set; }
    public string? AuthorEmail { get; protected set; }
    public string? Description { get; protected set; }
    public string? IconName { get; protected set; }
    public string? CoverPhoto { get; protected set; }
    public string Template { get; protected set; }
    public string? Attributes { get; protected set; }
    public string? Example { get; protected set; }
    public bool IsRtl { get; protected set; }
    public Guid LangId { get; protected set; }
    public string? Keywords { get; protected set; }
    public Guid? ParentId { get; protected set; }
    public BlockStatus Status { get; protected set; }
    public string? ViewPath { get; protected set; }
    
    public DateTime CreatedDate { get; protected set; }
    public DateTime? LastUpdated { get; protected set; }
    public string? CreatedByUserId { get; protected set; }
    public string? LastUpdatedByUserId { get; protected set; }
    public string? CreatedByIp { get; protected set; }
    public string? LastUpdatedByIp { get; protected set; }
    #endregion

    #region Navigations

    public Language Language { get; protected set; }

    public ICollection<BlockMeta> Meta { get; protected set; }
        = new HashSet<BlockMeta>();

    #endregion


    #region Builders


    public static Block Create(
        CreateBlockCommand command, IIdyfaUserContext userContext,
        IBehlogApplicationContext appContext, ISystemDateTime dateTime)
    {
        command.ThrowExceptionIfArgumentIsNull(nameof(command));
        userContext.ThrowExceptionIfArgumentIsNull(nameof(userContext));
        appContext.ThrowExceptionIfArgumentIsNull(nameof(appContext));
        dateTime.ThrowExceptionIfArgumentIsNull(nameof(dateTime));

        var block = new Block
        {
            Id = Guid.NewGuid(),
            Attributes = command.Attributes,
            Author = command.Author?.CorrectYeKe().Trim()!,
            Category = command.Category?.Trim()!,
            Description = command.Description?.CorrectYeKe().Trim()!,
            Example = command.Example,
            Keywords = command.Keywords?.CorrectYeKe()!,
            Name = command.Name?.Trim()!,
            Status = BlockStatus.Enabled,
            Template = command.Template,
            Title = command.Title?.CorrectYeKe().Trim()!,
            BlockType = command.BlockType?.Trim()!,
            AuthorEmail = command.AuthorEmail?.Trim()!,
            CreatedDate = dateTime.UtcNow,
            CoverPhoto = command.CoverPhoto,
            IconName = command.IconName,
            IsRtl = command.IsRtl,
            LangId = command.LangId,
            ParentId = command.ParentId,
            ViewPath = command.ViewPath,
            CreatedByIp = appContext.IpAddress!
        };
        
        block.AddCreatedEvent();
        return block;
    }

    #endregion

    
    #region Events

    private void AddCreatedEvent()
    {
        var e = new BlockCreatedEvent
        {
            Id = Id,
            Attributes = Attributes,
            Author = Author,
            Category = Category,
            Description = Description,
            Example = Example,
            Keywords = Keywords,
            Name = Name,
            Status = Status,
            Template = Template,
            Title = Title,
            AuthorEmail = AuthorEmail,
            BlockType = BlockType,
            CoverPhoto = CoverPhoto,
            CreatedDate = CreatedDate,
            IconName = IconName,
            IsRtl = IsRtl,
            LangId = LangId,
            LastUpdated = LastUpdated,
            ParentId = ParentId,
            ViewPath = ViewPath,
            CreatedByIp = CreatedByIp,
            CreatedByUserId = CreatedByUserId,
            LastUpdatedByIp = LastUpdatedByIp,
            LastUpdatedByUserId = LastUpdatedByUserId,
            Meta = Meta?.Select(_ => _.MapToResult()).ToList()!
        };
        
        Enqueue(e);
    }

    #endregion
}