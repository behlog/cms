using Behlog.Core.Domain;

namespace Behlog.Cms.Domain;


public class ComponentFile : ValueObject
{
    
    private ComponentFile() { }
    

    private ComponentFile(Guid componentId, Guid fileId)
    {
        ComponentId = componentId;
        FileId = fileId;
    }

    public static ComponentFile New(Guid componentId, Guid fileId)
    {
        var file = new ComponentFile(componentId, fileId);
        return file;
    }

    public ComponentFile WithFileName(string fileName)
    {
        FileName = fileName.CorrectYeKe().Trim();
        return this;
    }

    public ComponentFile WithTitle(string? title)
    {
        Title = title?.CorrectYeKe().Trim();
        return this;
    }

    public ComponentFile WithDescription(string? description)
    {
        Description = description?.CorrectYeKe();
        return this;
    }
    
    #region props

    public Guid ComponentId { get; }
    public Guid FileId { get; }
    public FileUpload File { get; }
    public string? Title { get; protected set; }
    public string FileName { get; protected set; }
    public string? Description { get; protected set; }

    #endregion
    
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return ComponentId;
        yield return FileId;
        yield return FileName;
        yield return Title;
        yield return Description;
    }
}