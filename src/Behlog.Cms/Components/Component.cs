using Behlog.Core;
using Behlog.Core.Domain;

namespace Behlog.Cms.Domain;

/// <summary>
/// Represents Behlog's WebComponents.
/// </summary>
public partial class Component : AggregateRoot<Guid>, IHasMetadata
{
    private Component() : base()
    {
        _meta = new List<ComponentMeta>();
        _files = new List<ComponentFile>();
    }
    
    #region props
    
    public Guid WebsiteId { get; protected set; }
    public Guid LangId { get; protected set; }
    public string Name { get; protected set; }
    public string Title { get; protected set; }
    public string ComponentType { get; protected set; }
    public string Category { get; protected set; }
    public string? Attributes { get; protected set; }
    public string? Description { get; protected set; }
    public EntityStatusEnum Status { get; protected set; }
    public string? Author { get; protected set; }
    public string? AuthorEmail { get; protected set; }
    public Guid? ParentId { get; protected set; }
    public bool IsRtl { get; protected set; }
    public string? Keywords { get; protected set; }
    public string? ViewPath { get; protected set; }

    public DateTime CreatedDate { get; protected set; }
    public DateTime? LastUpdated { get; protected set; }
    public string? CreatedByUserId { get; protected set; }
    public string? LastUpdatedByUserId { get; protected set; }
    public string? CreatedByIp { get; protected set; }
    public string? LastUpdatedByIp { get; protected set; }
    
    #endregion
    
    #region Navigations

    public Website Website { get; protected set; }
    public Language Language { get; protected set; }

    private List<ComponentMeta> _meta;
    public IReadOnlyCollection<ComponentMeta> Meta => _meta;

    private List<ComponentFile> _files;
    public IReadOnlyCollection<ComponentFile> Files => _files;

    #endregion
}
