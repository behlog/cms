using Behlog.Core;
using Behlog.Core.Domain;

namespace Behlog.Cms.Domain;

public class ContentMeta : ValueObject
{
    public ContentMeta() { }

    public ContentMeta(
        Guid contentId, string metaKey, string value, 
        EntityStatus status, string description, string category, int orderNum)
    {
        ContentId = contentId;
        MetaKey = metaKey;
        Value = value;
        Status = status;
        Description = description;
        Category = category;
        OrderNum = orderNum;
    }
    
    public Guid ContentId { get; private set; }
    public string MetaKey { get; private set; }
    public string Value { get; private set; }
    public EntityStatus Status { get; private set; }
    public string Description { get; private set; }
    public string Category { get; private set; }
    public int OrderNum { get; private set; }
    
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return ContentId;
        yield return MetaKey;
        yield return Value;
        yield return Status;
        yield return Description;
        yield return Category;
        yield return OrderNum;
    }
}