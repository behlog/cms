using Behlog.Core;

namespace Behlog.Cms.Models;

public class TagResult  
{
    
    public Guid Id { get; protected set; } 
    public string Title { get; protected set; }
    public string Slug { get; protected set; }
    public Guid LangId { get; protected set; }
    public string LangCode { get; protected set; }
    public EntityStatus Status { get; protected set; }
    public DateTime CreatedDate { get; protected set; }
    public string CreatedByUserId { get; protected set; }
    public string CreatedByUserIp { get; protected set; }
    
    
}