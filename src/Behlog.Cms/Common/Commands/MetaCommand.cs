namespace Behlog.Cms.Commands;


public class MetaCommand
{
    public Guid OwnerId { get; set; }
    public string Title { get; set; }
    public string MetaKey { get; set; }
    public string? MetaValue { get; set; }
    public string? Category { get; set; }
    public int OrderNum { get; set; }
    public string? Description { get; set; }
    public bool Enabled { get; set; }
    public Guid? LangId { get; set; }
}