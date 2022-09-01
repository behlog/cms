using System;

namespace Behlog.Cms;

public class CreateContentCategoryArg 
{
    public string Title { get; }
    public string AltTitle { get; }
    public string Slug { get; }
    public Guid? ParentId { get; }
    public string Description { get; }
    public Guid? ContentTypeId { get; }
}

public class UpdateContentCategoryArg 
{
    public string Title { get; }
    public string AltTitle { get; }
    public string Slug { get; }
    public Guid? ParentId { get; }
    public string Description { get; }
    public Guid? ContentTypeId { get; }
}