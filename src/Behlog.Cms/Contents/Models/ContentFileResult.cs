using Behlog.Cms.Domain;

namespace Behlog.Cms.Models;


public class ContentFileResult
{
    public ContentFileResult(
        Guid contentId, Guid fileId, string? title, string fileName, string? description)
    {
        ContentId = contentId;
        FileId = fileId;
        Title = title;
        FileName = fileName;
        Description = description;
    }

    public ContentFileResult WithFile(FileUpload? file)
    {
        if (file is null) return this;

        //TODO : is this best solution!? to get the FilePath working in all situations.
        FilePath = file.FilePath?.Replace("~", "");
        AlternateFilePath = file.AlternateFilePath?.Replace("~", "");
        Extension = file.Extension;
        FileSize = file.FileSize;
        AltTitle = file.AltTitle;
        Url = file.Url;
        FileType = file.FileType;
        return this;
    }
    
    public Guid ContentId { get; }
    public Guid FileId { get; }
    public string? Title { get; }
    public string FileName { get; }
    public string? Description { get; }
    public string? FilePath { get; private set; }
    public string? AlternateFilePath { get; private set; }
    public string? Extension { get; private set; }
    public long? FileSize { get; private set; }
    public string? AltTitle { get; private set; }
    public string? Url { get; private set; }
    public FileTypeEnum? FileType { get; private set; }
}