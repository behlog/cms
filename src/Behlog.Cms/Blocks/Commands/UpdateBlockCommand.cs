using Behlog.Core;
using Behlog.Core.Models;

namespace Behlog.Cms.Commands;

public class UpdateBlockCommand : IBehlogCommand<CommandResult>
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Title { get; set; }
    public string BlockType { get; set; }
    public string Category { get; set; }
    public string Author { get; set; }
    public string AuthorEmail { get; set; }
    public string Description { get; set; }
    public string IconName { get; set; }
    public string CoverPhoto { get; set; }
    public string Template { get; set; }
    public string Example { get; set; }
    public string Attributes { get; set; }
    public bool IsRtl { get; set; }
    public Guid LangId { get; set; }
    public string Keywords { get; set; }
    public Guid? ParentId { get; set; }
    public bool Enabled { get; set; }
    public string ViewPath { get; set; }
}