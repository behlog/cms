namespace Behlog.Cms.Commands;


public class ContentFileCommand
{
    public ContentFileCommand(
        Guid fileId, string fileName, string? title, string? description = null)
    {
        FileId = fileId;
        FileName = fileName;
        Title = title;
        Description = description;
    }
    
    public Guid FileId { get; }
    public string FileName { get; }
    public string? Title { get; }
    public string? Description { get; }
}