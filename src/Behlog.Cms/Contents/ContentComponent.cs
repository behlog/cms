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

    public Guid ContentId { get; protected set; }
    public Guid ComponentId { get; protected set; }
    public Component Component { get; protected set; }
    public int OrderNum { get; protected set; }
    public EntityStatus Status { get; protected set; }

    #endregion

    protected override IEnumerable<object> GetEqualityComponents() {
        yield return ContentId;
        yield return ComponentId;
        yield return OrderNum;
        yield return Status;
    }
}