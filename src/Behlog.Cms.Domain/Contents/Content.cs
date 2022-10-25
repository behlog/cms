using Behlog.Core;
using Behlog.Cms.Core;
using Behlog.Cms.Events;
using Behlog.Extensions;
using Behlog.Core.Domain;
using Behlog.Cms.Commands;
using Behlog.Cms.Domain.Models;

namespace Behlog.Cms.Domain;

public class Content : BehlogEntity<Guid>, IHasMetadata
{
    protected AggregateCompletionTask CompletionTask = new();

    private Content() : base()
    {
        
    }
    
    #region Props

    public string Title { get; protected set; }
    public string Slug { get; protected set; }
    public Guid ContentTypeId { get; protected set; }
    public string Body { get; protected set; }
    public ContentBodyType BodyType { get; protected set; }
    public string AuthorUserId { get; protected set; }
    public string Summary { get; protected set; }
    public ContentStatus Status { get; protected set; }
    public DateTime? PublishDate { get; protected set; }
    public string AltTitle { get; protected set; }
    public int OrderNum { get; protected set; }
    public IReadOnlyCollection<Guid> Categories { get; protected set; } = new List<Guid>();


    public DateTime CreatedDate { get; protected set; }
    public DateTime? LastUpdated { get; protected set; }
    public string CreatedByUserId { get; protected set; }
    public string LastUpdatedByUserId { get; protected set; }
    public string CreatedByIp { get; protected set; }
    public string LastUpdatedByIp { get; protected set; }
    #endregion

    #region Builders

    public static async Task<Content> CreateAsync(
        CreateContentCommand command, IBehlogManager manager)
    {
        command.ThrowExceptionIfArgumentIsNull(nameof(command));
        manager.ThrowExceptionIfArgumentIsNull(nameof(manager));

        var content = new Content
        {
            Id = Guid.NewGuid(),
            CreatedDate = DateTime.UtcNow, //TODO : from dateservice
            Title = command.Title.Trim().CorrectYeKe(),
            AltTitle = command.AltTitle?.Trim().CorrectYeKe()!,
            Slug = command.Slug?.MakeSlug().CorrectYeKe()!,
            ContentTypeId = command.ContentTypeId,
            Body = command.Body?.CorrectYeKe()!,
            AuthorUserId = command.AuthorUserId,
            Summary = command.Summary?.CorrectYeKe()!,
            OrderNum = command.OrderNum,
            Categories = command.Categories?.ToList()!,
            Status = ContentStatus.Draft,
            BodyType = command.BodyType,
            CreatedByIp = "", //TODO : read from UserContext
            CreatedByUserId = "", //TODO : read from UserContext
        };

        await content.PublishCreatedEvent(manager);
        return await Task.FromResult(content);
    }
    
    public async Task UpdateAsync(
        UpdateContentCommand command, IBehlogManager manager)
    {
        command.ThrowExceptionIfArgumentIsNull(nameof(command));
        manager.ThrowExceptionIfArgumentIsNull(nameof(manager));
        
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
        Categories = command.Categories;
        LastUpdated = DateTime.UtcNow; //TODO : use date service
        
        await PublishUpdatedEvent(manager);
    }


    public async Task PublishContentAsync(IBehlogManager manager)
    {
        Status = ContentStatus.Published;
        PublishDate = DateTime.UtcNow;
        
        var publishDate = DateTime.UtcNow;
        var userId = "";
        var userIp = "";

        var e = new ContentPublishedEvent(
            Id, publishDate, userId, userIp);
        await manager.PublishAsync(e).ConfigureAwait(false);
    }

    #endregion
    
    #region Events

    private async Task PublishCreatedEvent(IBehlogManager manager)
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
            categories: Categories
        );

        await manager.PublishAsync(e).ConfigureAwait(false);
    }

    private async Task PublishUpdatedEvent(IBehlogManager manager) 
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
            categories: Categories
        );

        await manager.PublishAsync(e).ConfigureAwait(false);
    }

    private async Task PublishRemovedEvent(IBehlogManager manager)
    {
        var e = new ContentRemovedEvent(Id);
        await manager.PublishAsync(e).ConfigureAwait(false);
    }

    #endregion
}