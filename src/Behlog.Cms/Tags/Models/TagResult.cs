using Behlog.Core;

namespace Behlog.Cms.Models;

public class TagResult  
{
    
    public Guid Id { get; set; } 
    public string Title { get; set; }
    public string Slug { get; set; }
    public Guid LangId { get; set; }
    public string? LangCode { get; set; }
    public EntityStatus Status { get; set; }
    public DateTime CreatedDate { get; set; }
    public string? CreatedByUserId { get; set; }
    public string? CreatedByUserIp { get; set; }
    
    
}