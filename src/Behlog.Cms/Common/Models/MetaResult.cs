using Behlog.Core;

namespace Behlog.Cms.Models;


public class MetaResult
{
    public MetaResult(
        Guid ownerId, string title, string metaKey, string metaValue, string metaType,
        EntityStatusEnum status, Guid? langId, string description, string category, int orderNum)
    {
        OwnerId = ownerId;
        Title = title;
        MetaKey = metaKey;
        MetaValue = metaValue;
        MetaType = metaType;
        Status = status;
        LangId = langId;
        Description = description;
        Category = category;
        OrderNum = orderNum;
    }
    
    public Guid OwnerId { get; }
    public string Title { get; }
    public string MetaKey { get; }
    public string MetaValue { get; }
    public string MetaType { get; }
    public EntityStatusEnum Status { get; }
    public Guid? LangId { get; }
    public string Description { get; }
    public string Category { get; }
    public int OrderNum { get; }
}