using System;
using System.Collections.Generic;
using Behlog.Cms.Domain.Events;
using Behlog.Core;
using Behlog.Extensions;

namespace Behlog.Cms.Domain;

public class Content : AggregateRoot<Guid>, IHasMetadata
{
    // private readonly IDateService _dateService = new DateService();

    protected Content(CreateContentArg args) : base()
    {
        if(args is null) throw new ArgumentNullException(nameof(args));

        Id = Guid.NewGuid();
        Title = args.Title;
        AltTitle = args.AltTitle;
        Slug = args.Slug?.MakeSlug();
        ContentTypeId = args.ContentTypeId;
        Body = args.Body;
        AuthorUserId = args.AuthorUserId;
        Summary = args.Summary;
        OrderNum = args.OrderNum;
        Categories = args.Categories;
        //Publish CreatedEvent


    }

    #region Methods

    public static Content Create(CreateContentArg args) {
        var content = new Content(args);
        
        // content.CreateDate = _dateService.UtcNow();

        //ApplyAndPublish CreatedEvent
        
        return content;
    }

    #endregion

    #region Props

    public string Title { get; }
    public string Slug { get; }
    public Guid ContentTypeId { get; }
    public string Body { get; }
    public ContentBodyType BodyType { get; }
    public string AuthorUserId { get; }
    public string Summary { get; }
    public ContentStatus Status { get; }
    public string AltTitle { get; }
    public int OrderNum { get; }
    public IReadOnlyCollection<Guid> Categories { get; } = new List<Guid>();


    public DateTime CreatedDate { get; }
    public DateTime? LastUpdated { get; }
    public string CreatedByUserId { get; }
    public string LastUpdatedByUserId { get; }
    public string CreatedByIp { get; }
    public string LastUpdatedByIp { get; }
    #endregion
    
    #region Events

    private void publishCreatedEvent()
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

        //TODO : publish the event
    }

    private void publishUpdatedEvent() 
    {
        var e = new ContentUpdatedEvent(
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

        //TODO : publish the event
    }

    #endregion
}