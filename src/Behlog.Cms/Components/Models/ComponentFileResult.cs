using Behlog.Cms.Domain;

namespace Behlog.Cms.Models;

public class ComponentFileResult
{

    public ComponentFileResult(
        Guid componentId, Guid fileId, string? title, string fileName, string? description)
    {
        ComponentId = componentId;
        FileId = fileId;
        Title = title;
        FileName = fileName;
        Description = description;
    }


    public ComponentFileResult WithFile(FileUpload? file)
    {
        if (file is null) return this;

        FilePath = file.FilePath;
        AlternateFilePath = file.AlternateFilePath;
        Extension = file.Extension;
        FileSize = file.FileSize;
        AltTitle = file.AltTitle;
        Url = file.Url;
        FileType = file.FileType;
        return this;
    }
    
    public Guid ComponentId { get; }
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