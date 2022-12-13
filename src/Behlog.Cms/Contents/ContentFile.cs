using Behlog.Core.Domain;
using Idyfa.Core.Extensions;

namespace Behlog.Cms.Domain;

public class ContentFile : ValueObject
{

    private ContentFile()
    {
    }
    
    private ContentFile(Guid contentId, Guid fileId)
    {
        ContentId = contentId;
        FileId = fileId;
    }

    private ContentFile(
        Guid contentId, Guid fileId, string title,
        string fileName, string description)
    {
        ContentId = contentId;
        FileId = fileId;
        Title = title;
        FileName = fileName;
        Description = description;
    }
    
    public Guid ContentId { get; }
    public Guid FileId { get; }
    public FileUpload File { get; }
    public string? Title { get; protected set; }
    public string FileName { get; protected set; }
    public string? Description { get; protected set; }

    #region Builders

    public static ContentFile New(Guid contentId, Guid fileId)
    {
        var contentFile = new ContentFile(contentId, fileId);
        return contentFile;
    }

    public ContentFile WithFileName(string fileName)
    {
        FileName = fileName;
        return this;
    }

    public ContentFile WithTitle(string? title)
    {
        Title = title?.CorrectYeKe().Trim();
        return this;
    }

    public ContentFile WithDescription(string? description)
    {
        Description = description?.CorrectYeKe();
        return this;
    }

    #endregion
    
    
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return ContentId;
        yield return FileId;
        yield return FileName;
        yield return Title;
        yield return Description;
    }
    
    
}