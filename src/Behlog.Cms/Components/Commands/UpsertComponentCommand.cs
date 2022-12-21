using Behlog.Cms.Models;
using Behlog.Core;
using Behlog.Core.Models;

namespace Behlog.Cms.Commands;


public class UpsertComponentCommand : IBehlogCommand<CommandResult<ComponentResult>>
{
    public UpsertComponentCommand()
    {
        Meta = new List<MetaCommand>();
        Files = new List<ComponentFileCommand>();
    }
    
    public Guid WebsiteId { get; set; }
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
    public bool IsRtl { get; set; }
    public string? Keywords { get; set; }
    public string? ViewPath { get; set; }
    public ICollection<MetaCommand> Meta { get; set; }
    public ICollection<ComponentFileCommand> Files { get; set; }
    
    
    public CreateComponentCommand ConvertToCreateCommand()
    {
        return new CreateComponentCommand
        {
            Attributes = this.Attributes,
            Author = this.Author,
            Category = this.Category,
            Description = this.Description,
            Files = this.Files,
            Keywords = this.Keywords,
            Meta = this.Meta,
            Name = this.Name,
            Title = this.Title,
            AuthorEmail = this.AuthorEmail,
            ComponentType = this.ComponentType,
            IsRtl = this.IsRtl,
            LangId = this.LangId,
            ParentId = this.ParentId,
            ViewPath = this.ViewPath,
            WebsiteId = this.WebsiteId
        };
    }
    
    
    public UpdateComponentCommand ConvertToUpdateCommand(Guid id)
    {
        return new UpdateComponentCommand
        {
            Id = id,
            Attributes = this.Attributes,
            Author = this.Author,
            Category = this.Category,
            Description = this.Description,
            Enabled = true,
            Files = this.Files,
            Keywords = this.Keywords,
            Meta = this.Meta,
            Name = this.Name,
            Title = this.Title,
            AuthorEmail = this.AuthorEmail,
            ComponentType = this.ComponentType,
            IsRtl = this.IsRtl,
            LangId = this.LangId,
            ParentId = this.ParentId,
            ViewPath = this.ViewPath
        };
    }
}