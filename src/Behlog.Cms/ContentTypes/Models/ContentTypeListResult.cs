namespace Behlog.Cms.Models;

/// <summary>
/// Represents list of <see cref="ContentTypeResult"/> for a specific <see cref="Language"/>.
/// </summary>
public class ContentTypeListResult
{
    private Dictionary<Guid, ContentTypeResult> _dictionary = new();

    public ContentTypeListResult(Guid langId, string langCode, string langTitle)
    {
        LangId = langId;
        LangCode = langCode;
        LangTitle = langTitle;
    }

    public Guid LangId { get; }
    
    public string LangCode { get; }
    
    public string LangTitle { get; }
    
    public ContentTypeListResult(
        Guid langId, string langCode, string langTitle,
        IReadOnlyCollection<ContentTypeResult> results)
    {
        LangId = langId;
        LangCode = langCode;
        LangTitle = langTitle;
        
        Add(results);
    }

    public ContentTypeResult this[Guid index]
    {
        get => _dictionary[index];
        set => _dictionary[index] = value;
    }

    public void Add(ContentTypeResult contentType)
    {
        contentType.ThrowExceptionIfArgumentIsNull(nameof(contentType));
        _dictionary.Add(contentType.Id, contentType);
    }

    public void Add(IReadOnlyCollection<ContentTypeResult> contentTypes)
    {
        contentTypes.ThrowExceptionIfArgumentIsNull(nameof(contentTypes));
        if(!contentTypes.Any()) return;
        
        foreach(var item in contentTypes)
            Add(item);
    }

    public IReadOnlyCollection<ContentTypeResult> Items
        => _dictionary.Values;
}