namespace Behlog.Cms.Commands;

public class UpdateFileUploadCommand : IBehlogCommand<CommandResult>
{

    public UpdateFileUploadCommand(
        Guid id, string? title, IFormFile? alternateFileData, string? altTitle, 
        string? url, string? description, bool hidden)
    {
        Id = id;
        Title = title;
        AlternateFileData = alternateFileData;
        AltTitle = altTitle;
        Url = url;
        Description = description;
        Hidden = hidden;
    }
    
    public Guid Id { get; }
    public string? Title { get; }
    public string? AltTitle { get; }
    public bool Hidden { get; }
    public string? Url { get; }
    public string? Description { get; }
    public IFormFile? AlternateFileData { get; }
}