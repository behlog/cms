using Behlog.Core;
using Behlog.Cms.Core;
using Behlog.Cms.Events;
using Behlog.Extensions;
using Behlog.Core.Domain;
using Behlog.Cms.Commands;
using Behlog.Cms.Exceptions;
using Behlog.Core.Contracts;
using Idyfa.Core.Contracts;
using Behlog.Cms.Contracts;
using Idyfa.Core;

namespace Behlog.Cms.Domain;


public partial class Content : AggregateRoot<Guid>, IHasMetadata
{
    protected AggregateCompletionTask CompletionTask = new();

    private Content() { }

    #region Props
    
    public Guid WebsiteId { get; protected set; }
    public string Title { get; protected set; }
    public string Slug { get; protected set; }
    public Guid ContentTypeId { get; protected set; }
    public string? Body { get; protected set; }
    public Guid LangId { get; protected set; }
    public string? LangCode { get; protected set; }
    public ContentBodyType BodyType { get; protected set; }
    public string AuthorUserId { get; protected set; }
    public string? Summary { get; protected set; }
    public ContentStatus Status { get; protected set; }
    public DateTime? LastStatusChangedDate { get; protected set; }
    public DateTime? PublishDate { get; protected set; }
    public string? AltTitle { get; protected set; }
    public string? Password { get; protected set; }
    public int OrderNum { get; protected set; }
    public string? IconName { get; protected set; }
    public string? ViewPath { get; protected set; }
    public DateTime CreatedDate { get; protected set; } 
    public DateTime? LastUpdated { get; protected set; }
    public string? CreatedByUserId { get; protected set; }
    public string? LastUpdatedByUserId { get; protected set; }
    public string? CreatedByIp { get; protected set; }
    public string? LastUpdatedByIp { get; protected set; }
    #endregion

    #region Navigations

    public ContentType ContentType { get; protected set; }
    
    public Language Language { get; protected set; }
    
    public ICollection<ContentCategoryItem> Categories { get; protected set; } 
        = new HashSet<ContentCategoryItem>();

    public ICollection<ContentMeta> Meta { get; protected set; }
        = new HashSet<ContentMeta>();

    public ICollection<ContentFile> Files { get; protected set; } 
        = new HashSet<ContentFile>();
    
    public ICollection<ContentLike> Likes { get; protected set; }
        = new HashSet<ContentLike>();

    #endregion

    #region Builders
    
    
    public static async Task<Content> CreateAsync(
        CreateContentCommand command, IContentService service,
        IIdyfaUserContext userContext, IBehlogApplicationContext appContext,
        ISystemDateTime dateTime)
    {
        command.ThrowExceptionIfArgumentIsNull(nameof(command));
        service.ThrowExceptionIfArgumentIsNull(nameof(service));
        userContext.ThrowExceptionIfArgumentIsNull(nameof(userContext));
        appContext.ThrowExceptionIfArgumentIsNull(nameof(appContext));
        dateTime.ThrowExceptionIfArgumentIsNull(nameof(dateTime));

        await CheckForDuplicateSlug(null, command.WebsiteId, command.Slug, service);

        var content = new Content
        {
            Id = Guid.NewGuid(),
            CreatedDate = dateTime.UtcNow,
            Title = command.Title.Trim().CorrectYeKe(),
            AltTitle = command.AltTitle?.Trim().CorrectYeKe()!,
            Slug = command.Slug?.MakeSlug().CorrectYeKe()!,
            ContentTypeId = command.ContentTypeId,
            Body = command.Body?.CorrectYeKe()!,
            AuthorUserId = userContext.UserId!,
            Summary = command.Summary?.CorrectYeKe()!,
            OrderNum = command.OrderNum,
            Status = ContentStatus.Draft,
            BodyType = command.BodyType,
            CreatedByIp = appContext.IpAddress, 
            CreatedByUserId = userContext.UserId,
            Password = command.Password,
            IconName = command.IconName,
            ViewPath = command.ViewPath,
            LangId = command.LangId,
            WebsiteId = command.WebsiteId,
            LangCode = "" //TODO : add langcode
        };

        content.Categories = command.Categories.Convert(content.Id);
        content.Meta = command.Meta.Convert(content.Id);
        content.Files = command.Files.Convert(content.Id);
        
        content.AddCreatedEvent();
        
        return await Task.FromResult(content);
    }
    
    public async Task UpdateAsync(
        UpdateContentCommand command, IContentService service,
        IIdyfaUserContext userContext, ISystemDateTime dateTime, 
        IBehlogApplicationContext appContext)
    {
        command.ThrowExceptionIfArgumentIsNull(nameof(command));
        service.ThrowExceptionIfArgumentIsNull(nameof(service));
        userContext.ThrowExceptionIfArgumentIsNull(nameof(userContext));
        dateTime.ThrowExceptionIfArgumentIsNull(nameof(dateTime));
        appContext.ThrowExceptionIfArgumentIsNull(nameof(appContext));

        await CheckForDuplicateSlug(Id, WebsiteId, command.Slug, service);
        
        Title = command.Title.Trim().CorrectYeKe();
        Slug = command.Slug?.MakeSlug().CorrectYeKe()!;
        ContentTypeId = command.ContentTypeId;
        Body = command.Body?.CorrectYeKe()!;
        BodyType = command.BodyType;
        AuthorUserId = command.AuthorUserId;
        Summary = command.Summary?.CorrectYeKe()!;
        Status = command.Status;
        AltTitle = command.AltTitle?.Trim().CorrectYeKe()!;
        OrderNum = command.OrderNum;
        Categories = command.Categories.Convert(Id);
        LastUpdated = dateTime.UtcNow;
        LastUpdatedByUserId = userContext.UserId;
        IconName = command.IconName;
        Password = command.Password;

        Categories = command.Categories.Convert(Id);
        Meta = command.Meta.Convert(Id);

        AddUpdatedEvent();
    }

    /// <summary>
    /// Mark a <see cref="Content"/> as <see cref="ContentStatus.Deleted"/>.
    /// When soft deleted, the Content wont displayed anymore and the user must
    /// find it on recycle bin.
    /// </summary>
    public async Task SoftDeleteAsync(
        IIdyfaUserContext userContext, ISystemDateTime dateTime)
    {
        userContext.ThrowExceptionIfArgumentIsNull(nameof(userContext));
        dateTime.ThrowExceptionIfArgumentIsNull(nameof(dateTime));
        
        Status = ContentStatus.Deleted;
        LastStatusChangedDate = dateTime.UtcNow;
        LastUpdatedByUserId = userContext.UserId;
        
        var e = new ContentSoftDeletedEvent(Id);
        Enqueue(e);
    }


    public async Task PublishContentAsync(
        IIdyfaUserContext userContext, ISystemDateTime dateTime,
        IBehlogApplicationContext appContext)
    {
        userContext.ThrowExceptionIfArgumentIsNull(nameof(userContext));
        dateTime.ThrowExceptionIfArgumentIsNull(nameof(dateTime));
        appContext.ThrowExceptionIfArgumentIsNull(nameof(appContext));
        
        if (!Status.CanPublished())
        {
            throw new ContentCannotPublishedException(Status);
        }
        
        Status = ContentStatus.Published;
        var publishDate = dateTime.UtcNow;
        PublishDate = publishDate;
        LastStatusChangedDate = publishDate;
        
        var userId = userContext.UserId!;
        var userIp = appContext.IpAddress!;

        var e = new ContentPublishedEvent(Id, publishDate, userId, userIp);
        Enqueue(e);
    }

    
    /// <summary>
    /// Removing the <see cref="Content"/> physically. When Removed, the data cannot recovered.
    /// </summary>
    public void Remove()
    {
        AddRemovedEvent();
    }
    
    #endregion
    
    #region Events

    private void AddCreatedEvent()
    {
        var e = new ContentCreatedEvent(
            id: Id,
            title: Title,
            slug: Slug,
            contetTypeId: ContentTypeId,
            body: Body,
            bodyType: BodyType,
            authorUserId: AuthorUserId,
            summary: Summary,
            status: Status,
            altTitle: AltTitle,
            orderNum: OrderNum,
            categories: Categories?.Select(_=> _.CategoryId).ToList()!
        );

        Enqueue(e);
    }

    private void AddUpdatedEvent() 
    {
        var e = new ContentUpdatedEvent(
            id: Id,
            title: Title,
            slug: Slug,
            contentTypeId: ContentTypeId,
            body: Body,
            bodyType: BodyType,
            authorUserId: AuthorUserId,
            summary: Summary,
            status: Status,
            altTitle: AltTitle,
            orderNum: OrderNum,
            categories: Categories?.Select(_=> _.CategoryId).ToList()!
        );

        Enqueue(e);
    }

    private void AddRemovedEvent()
    {
        var e = new ContentRemovedEvent(Id);
        Enqueue(e);
    }

    #endregion
}