namespace Behlog.Cms.Commands;

public class ComponentFileCommand
{

    public ComponentFileCommand(
        Guid fileId, string fileName, string? title, string? description = null)
    {
        FileId = fileId;
        FileName = fileName;
        Title = title;
        Description = description;
    }
    
    public Guid FileId { get; }
    public string FileName { get; private set; }
    public string? Title { get; private set; }
    public string? Description { get; private set; }

    public ComponentFileCommand WithFileName(string fileName)
    {
        FileName = fileName;
        return this;
    }

    public ComponentFileCommand WithTitle(string? title)
    {
        Title = title;
        return this;
    }

    public ComponentFileCommand WithDescription(string? description)
    {
        Description = description;
        return this;
    }
}