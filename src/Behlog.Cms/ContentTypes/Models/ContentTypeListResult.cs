using Behlog.Extensions;

namespace Behlog.Cms.Models;

public class ContentTypeListResult
{
    private Dictionary<Guid, ContentTypeResult> _dictionary = new();

    public ContentTypeListResult()
    {
    }

    public ContentTypeListResult(IReadOnlyCollection<ContentTypeResult> results)
    {
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
}