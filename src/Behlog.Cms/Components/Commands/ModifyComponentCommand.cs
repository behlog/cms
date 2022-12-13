using Behlog.Core;
using Behlog.Core.Models;

namespace Behlog.Cms.Commands;


public class ModifyComponentCommand : IBehlogCommand<CommandResult>
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Title { get; set; }
    public string Category { get; set; }
    public string? Description { get; set; }
    public string? Author { get; set; }
    public string? AuthorEmail { get; set; }
    public Guid? ParentId { get; set; }
    public bool Enabled { get; set; }
}