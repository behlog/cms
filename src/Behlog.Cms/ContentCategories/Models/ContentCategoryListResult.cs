using Behlog.Extensions;

namespace Behlog.Cms.Models;

public class ContentCategoryListResult
{
    private Dictionary<Guid, ContentCategoryResult> _dictionary = new();

    public ContentCategoryListResult(IReadOnlyCollection<ContentCategoryResult> items)
    {
        Add(items);
    }


    public ContentCategoryResult this[Guid index]
    {
        get => _dictionary[index];
        set => _dictionary[index] = value;
    }

    public int TotalCount => _dictionary.Count;
    
    private void Add(ContentCategoryResult item)
    {
        item.ThrowExceptionIfArgumentIsNull(nameof(item));
        _dictionary.Add(item.Id, item);
    }

    private void Add(IReadOnlyCollection<ContentCategoryResult> items)
    {
        foreach (var item in items)
        {
            Add(item);
        }
    }


    public IReadOnlyCollection<ContentCategoryResult> GetByParentId(Guid? parentId)
    {
        return _dictionary.Values.Where(_ => _.ParentId == parentId).ToList();
    }
}