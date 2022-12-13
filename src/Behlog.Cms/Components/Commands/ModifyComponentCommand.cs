using Behlog.Core;
using Behlog.Core.Models;

namespace Behlog.Cms.Commands;

public class ModifyComponentCommand : IBehlogCommand<CommandResult>
{
    public ModifyComponentCommand()
    {
        Meta = new List<MetaCommand>();
        Files = new List<ComponentFileCommand>();
    }
    
    public Guid Id { get; set; }
    public Guid LangId { get; set; }
    public string Name { get; set; }
    public string Title { get; set; }
    public string ComponentType { get; set; }
    public string Category { get; set; }
    public string? Attributes { get; set; }
    public string? Description { get; set; }
    public string? Author { get; set; }
    public string? AuthorEmail { get; set; }
    public Guid? ParentId { get; set; }
    public bool Enabled { get; set; }
    public bool IsRtl { get; set; }
    public string? Keywords { get; set; }
    public string? ViewPath { get; set; }
    public ICollection<MetaCommand> Meta { get; set; }
    public ICollection<ComponentFileCommand> Files { get; set; }
}