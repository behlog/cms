using Behlog.Cms.Domain;
using Behlog.Core;
using Behlog.Cms.Models;

namespace Behlog.Cms.Commands;


public class CreateFileUploadCommand : IBehlogCommand<FileUploadResult>
{
    public CreateFileUploadCommand(
        IFormFile fileData, string contentType, FileType fileType, Guid websiteId, 
        string title, string description)
    {
        FileData = fileData;
        FileType = fileType;
        WebsiteId = websiteId;
        Title = title;
        Description = description;
        ContentType = contentType;
    }

    public CreateFileUploadCommand(
        IFormFile fileData, FileType fileType, Guid websiteId, 
        IFormFile alternateFileData, string title, string altTitle, string description)
    {
        FileData = fileData;
        FileType = fileType;
        WebsiteId = websiteId;
        AlternateFileData = alternateFileData;
        Title = title;
        AltTitle = altTitle;
        Description = description;
    }

    public IFormFile FileData { get; }
    public FileType FileType { get; }
    public string Title { get; }
    public IFormFile AlternateFileData { get; }
    public string AltTitle { get; }
    public string Description { get; }
    public Guid WebsiteId { get; }
    public string ContentType { get; }
}


public class CreateFileWithUrlCommand : IBehlogCommand<FileUploadResult>
{
    public CreateFileWithUrlCommand(
        string url, FileType fileType, Guid websiteId, 
        string title, string altTitle, string description)
    {
        Url = url;
        FileType = fileType;
        WebsiteId = websiteId;
        Title = title;
        AltTitle = altTitle;
        Description = description;
    }
    
    public string Title { get; }
    public string AltTitle { get; }
    public string Url { get; }
    public FileType FileType { get; }
    public Guid WebsiteId { get; }
    public string Description { get; }
}