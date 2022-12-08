using Behlog.Core;
using Behlog.Core.Domain;

namespace Behlog.Cms.Domain;

/// <summary>
/// Represents Behlog's Web Components.
/// </summary>
public class Component : AggregateRoot<Guid>
{
    private Component() : base() { }


    #region props
    public Guid WebsiteId { get; protected set; }
    public string Name { get; protected set; }
    public string Title { get; protected set; }
    public string Category { get; protected set; }
    public string? Description { get; protected set; }
    public EntityStatus Status { get; protected set; }
    public string? Author { get; protected set; }
    public string? AuthorEmail { get; protected set; }
    public Guid? ParentId { get; protected set; }
    public DateTime CreatedDate { get; protected set; }
    public DateTime? LastUpdated { get; protected set; }
    #endregion


    #region Navigations

    public Website Website { get; protected set; }

    #endregion
}
