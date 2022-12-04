using Behlog.Cms.Domain;
using Behlog.Cms.Models;
using Behlog.Core;

namespace Behlog.Cms.Events;


public class BlockCreatedEvent : IBehlogEvent
{

    public BlockCreatedEvent()
    {
        Meta = new List<MetaResult>();
    }
    
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Title { get; set; }
    public string BlockType { get; set; }
    public string? Category { get; set; }
    public string? Author { get; set; }
    public string? AuthorEmail { get; set; }
    public string? Description { get; set; }
    public string? IconName { get; set; }
    public string? CoverPhoto { get; set; }
    public string Template { get; set; }
    public string? Example { get; set; }
    public string? Attributes { get; set; }
    public bool IsRtl { get; set; }
    public Guid LangId { get; set; }
    public string? Keywords { get; set; }
    public Guid? ParentId { get; set; }
    public BlockStatus Status { get; set; }
    public string? ViewPath { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime? LastUpdated { get; set; }
    public string CreatedByUserId { get; set; }
    public string LastUpdatedByUserId { get; set; }
    public string CreatedByIp { get; set; }
    public string LastUpdatedByIp { get; set; }
    public IReadOnlyCollection<MetaResult> Meta { get; set; }
}