using System;
using Behlog.Core;

namespace Behlog.Cms;

public class Content : AggregateRoot<Guid>
{
    // private readonly IDateService _dateService = new DateService();

    protected Content(CreateContentArg args) : base()
    {
        if(args is null) throw new ArgumentNullException(nameof(args));
        Id = Guid.NewGuid();
        Title = args.Title;
        AltTitle = args.AltTitle;
        Slug = args.Slug;
        ContentTypeId = args.ContentTypeId;
        Body = args.Body;
        AuthorUserId = args.AuthorUserId;
        Summary = args.Summary;
        OrderNum = args.OrderNum;

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
    public string AuthorUserId { get; }
    public string Summary { get; }
    public ContentStatus Status { get; }
    public string AltTitle { get; }
    public int OrderNum { get; }
    
    #endregion
    
}