using System;
using Behlog.Core;

namespace Behlog.Cms;

public class ContentCategory : AggregateRoot<Guid> 
{

    protected ContentCategory() : base()
    {

    }

    #region Props
    public string Title { get; }
    public string AltTitle { get; }
    public string Slug { get; }
    public Guid? ParentId { get; }
    public string Description { get; }
    public Guid? ContentTypeId { get; }
    public EntityStatus Status { get; }
    #endregion
}