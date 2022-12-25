using Behlog.Core;

namespace Behlog.Cms.Models;


public class ContentTypeResult
{
    public Guid Id { get; set; }
    public string SystemName { get; set; }
    public string Title { get; set; }
    public string Slug { get; set; }
    public string? Description { get; set; }
    public Guid LangId { get; set; }
    public EntityStatusEnum Status { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime? LastUpdated { get; set; }
    public DateTime? LastStatusChangedOn { get; set; }
    
    public string? LangCode { get; set; }
    public string? LangName { get; set; }
    public string? LangTitle { get; set; }
}