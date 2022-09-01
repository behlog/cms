using System;

namespace Behlog.Cms;

public class CreateContentArg 
{
    public string Title { get; }
    public string Slug { get; }
    public Guid ContentTypeId { get; }
    public string Body { get; }
    public string AuthorUserId { get; }
    public string Summary { get; }
    public string AltTitle { get; }
    public int OrderNum { get; }
}

public class UpdateContentArg 
{
    public Guid Id { get; }
    public string Title { get; }
    public string Slug { get; }
    public string Body { get; }
    public string AuthorUserId { get; }
    public string Summary { get; }
    public string AltTitle { get; }
    public int OrderNum { get; }
}