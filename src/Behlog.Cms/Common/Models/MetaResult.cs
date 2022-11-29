using Behlog.Core;

namespace Behlog.Cms.Models;


public class MetaResult
{
    public Guid OwnerId { get; set; }
    public string Title { get; set; }
    public string MetaKey { get; set; }
    public string MetaValue { get; set; }
    public string MetaType { get; set; }
    public EntityStatus Status { get; set; }
    public Guid? LangId { get; set; }
    public string LangCode { get; set; }
    public string Description { get; set; }
    public string Category { get; set; }
    public int OrderNum { get; set; }
}