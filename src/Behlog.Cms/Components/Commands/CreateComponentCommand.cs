using Behlog.Cms.Models;
using Behlog.Core;
using Behlog.Core.Models;

namespace Behlog.Cms.Commands;

public class CreateComponentCommand : IBehlogCommand<CommandResult<ComponentResult>>
{
    public Guid WebsiteId { get; set; }
    public string Name { get; set; }
    public string Title { get; set; }
    public string Category { get; set; }
    public string? Description { get; set; }
    public string? Author { get; set; }
    public string? AuthorEmail { get; set; }
    public Guid? ParentId { get; set; }
}