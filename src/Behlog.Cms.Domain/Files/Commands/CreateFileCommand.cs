using Behlog.Cms.Models;
using Behlog.Core;

namespace Behlog.Cms.Commands;

public class CreateFileCommand : IBehlogCommand<FileResult>
{
    public CreateFileCommand(
        string filePath, string title, string description)
    {
        FilePath = filePath;
        Title = title;
        Description = description;
    }

    public CreateFileCommand(
        string filePath, string alternateFilePath, 
        string title, string altTitle, string description)
    {
        FilePath = filePath;
        AlternateFilePath = alternateFilePath;
        Title = title;
        AltTitle = altTitle;
        Description = description;
    }

    
    public string Title { get; }
    public string FilePath { get; }
    public string AlternateFilePath { get; }
    public string AltTitle { get; }
    public string Description { get; }
}


public class CreateFileWithUrlCommand : IBehlogCommand<FileResult>
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