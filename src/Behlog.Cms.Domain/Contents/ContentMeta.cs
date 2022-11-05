using Behlog.Core;
using Behlog.Core.Models;

namespace Behlog.Cms.Domain;

public class ContentMeta : MetaBase<Guid>
{
    public ContentMeta() { }

    public ContentMeta(
        Guid ownerId, string metaKey, string metaValue, 
        EntityStatus status, string description, string category, 
        int orderNum)
    {
        OwnerId = ownerId;
        MetaKey = metaKey;
        Value = metaValue;
        Status = status;
        Description = description;
        Category = category;
        OrderNum = orderNum;
    }
    
    public string MetaKey { get; private set; }
    public string Value { get; private set; }
    public EntityStatus Status { get; private set; }
    public string Description { get; private set; }
    public string Category { get; private set; }
    public int OrderNum { get; private set; }
    
}