using iman.Domain;
using Behlog.Core;
using Behlog.Cms.Core;
using Behlog.Cms.Events;
using Behlog.Extensions;

namespace Behlog.Cms.Domain;

public class Content : AggregateRoot<Guid>, IHasMetadata
{
    protected AggregateCompletionTask CompletionTask = new();
    // private readonly IDateService _dateService = new DateService();

    protected Content(CreateContentArg args, IMediator mediator) : base(mediator)
    {
        if(args is null) throw new ArgumentNullException(nameof(args));

        // Id = Guid.NewGuid();
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
        CompletionTask.Add(
            publishCreatedEvent());
    }

    #region Methods

    public static async Task<Content> CreateAsync(CreateContentArg args, IMediator mediator) {
        var content = new Content(args, mediator);
        await (Task)content.CompletionTask;
        return content;
    }

    public async Task UpdateAsync(UpdateContentArg args)
    {
        if (args is null) throw new ArgumentNullException(nameof(args));

        // Id = args.Id;
        Title = args.Title.Trim().CorrectYeKe();
        Slug = args.Slug?.MakeSlug();
        ContentTypeId = args.ContentTypeId;
        Body = args.Body;
        BodyType = args.BodyType;
        AuthorUserId = args.AuthorUserId;
        Summary = args.Summary;
        Status = args.Status;
        AltTitle = args.AltTitle?.Trim().CorrectYeKe();
        OrderNum = args.OrderNum;
        Categories = args.Categories;

        await publishUpdatedEvent();
    }

    #endregion

    #region Props

    public string Title { get; protected set; }
    public string Slug { get; protected set; }
    public Guid ContentTypeId { get; protected set; }
    public string Body { get; protected set; }
    public ContentBodyType BodyType { get; protected set; }
    public string AuthorUserId { get; protected set; }
    public string Summary { get; protected set; }
    public ContentStatus Status { get; protected set; }
    public string AltTitle { get; protected set; }
    public int OrderNum { get; protected set; }
    public IReadOnlyCollection<Guid> Categories { get; protected set; } = new List<Guid>();


    public DateTime CreatedDate { get; }
    public DateTime? LastUpdated { get; }
    public string CreatedByUserId { get; }
    public string LastUpdatedByUserId { get; }
    public string CreatedByIp { get; }
    public string LastUpdatedByIp { get; }
    #endregion
    
    #region Events

    private async Task publishCreatedEvent()
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

        await _mediator.PublishAsync(e).ConfigureAwait(false);
    }

    private async Task publishUpdatedEvent() 
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

        await _mediator.PublishAsync(e).ConfigureAwait(false);
    }

    private async Task publishRemovedEvent()
    {
        var e = new ContentRemovedEvent(Id);
        await _mediator.PublishAsync(e).ConfigureAwait(false);
    }

    #endregion
}