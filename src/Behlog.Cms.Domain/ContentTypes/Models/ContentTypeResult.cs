using Behlog.Core;

namespace Behlog.Cms.Models;

public class ContentTypeResult : BehlogResult
{
    public Guid Id { get; set; }
    public string SystemName { get; set; }
    public string Title { get; set; }
    public string Slug { get; set; }
    public string Description { get; set; }
    public string Lang { get; set; }
    public EntityStatus Status { get; set; }
    public DateTime CreateDate { get; set; }
    public DateTime? ModifyDate { get; set; }
    public DateTime? LastStatusChangedOn { get; set; }
}