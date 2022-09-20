using System;
using System.Collections.Generic;

namespace Behlog.Cms.Domain;

public class CreateContentArg 
{
    public string Title { get; set; }
    public string Slug { get; set; }
    public Guid ContentTypeId { get; set; }
    public string Body { get; set; }
    public ContentBodyType BodyType { get; set; }
    public string AuthorUserId { get; set; }
    public string Summary { get; set; }
    public string AltTitle { get; set; }
    public int OrderNum { get; set; }
    public IReadOnlyCollection<Guid> Categories { get; set; } = new List<Guid>();
}

public class UpdateContentArg 
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Slug { get; set; }
    public string Body { get; set; }
    public ContentBodyType BodyType { get; set; }
    public ContentStatus Status { get; set; }
    public Guid ContentTypeId { get; set; }
    public string AuthorUserId { get; set; }
    public string Summary { get; set; }
    public string AltTitle { get; set; }
    public int OrderNum { get; set; }
    public IReadOnlyCollection<Guid> Categories { get; set; } = new List<Guid>();
}