using System;
using System.Collections.Generic;
using Behlog.Core;

namespace Behlog.Cms.Events;

public class ContentCreatedEvent : DomainEvent
{

    public ContentCreatedEvent(
        string title,
        string slug,
        Guid contetTypeId,
        string body,
        ContentBodyType bodyType,
        string authorUserId,
        string summary,
        ContentStatus status,
        string altTitle,
        int orderNum,
        IReadOnlyCollection<Guid> categories
    )
    {
        Title = title;
        Slug = slug;
        ContentTypeId = contetTypeId;
        Body = body;
        BodyType = bodyType;
        AuthorUserId = authorUserId;
        Summary = summary;
        Status = status;
        AltTitle = altTitle;
        OrderNum = orderNum;
        Categories = categories;
    }

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
}