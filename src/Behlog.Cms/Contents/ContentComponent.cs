using Behlog.Core;
using Behlog.Core.Domain;

namespace Behlog.Cms.Domain;

/// <summary>
/// 
/// </summary>
public class ContentComponent : ValueObject
{
    protected ContentComponent() {  }

    #region props

    public long Id { get; protected set; }
    public Guid ContentId { get; protected set; }
    public Guid ComponentId { get; protected set; }
    public Component Component { get; protected set; }
    public int OrderNum { get; protected set; }
    public EntityStatus Status { get; protected set; }
    public bool IsRtl { get; protected set; }
    public string? ViewPath { get; protected set; }
    public string? Params { get; protected set; }

    #endregion

    protected override IEnumerable<object> GetEqualityComponents() {
        yield return Id;
        yield return ContentId;
        yield return ComponentId;
        yield return OrderNum;
        yield return Status;
        yield return ViewPath;
        yield return Params;
    }
}