using Behlog.Cms.Models;
using Behlog.Core;

namespace Behlog.Cms.Commands;

public class CreateFileUploadCommand : IBehlogCommand<FileUploadResult>
{
    public CreateFileUploadCommand(
        IFormFile fileData, string title, string description)
    {
        FileData = fileData;
        Title = title;
        Description = description;
    }

    public CreateFileUploadCommand(
        IFormFile fileData, IFormFile alternateFileData, 
        string title, string altTitle, string description)
    {
        fileData = fileData;
        AlternateFileData = alternateFileData;
        Title = title;
        AltTitle = altTitle;
        Description = description;
    }

    public IFormFile FileData { get; }
    public string Title { get; }
    public IFormFile AlternateFileData { get; }
    public string AltTitle { get; }
    public string Description { get; }
}


public class CreateFileWithUrlCommand : IBehlogCommand<FileUploadResult>
{
    public CreateFileWithUrlCommand(
        string title, string altTitle, string url, string description)
    {
        Title = title;
        AltTitle = altTitle;
        Url = url;
        Description = description;
    }   
    
    public string Title { get; }
    public string AltTitle { get; }
    public string Url { get; }
    public string Description { get; }
}