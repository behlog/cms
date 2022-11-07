using Behlog.Core.Domain;

namespace Behlog.Cms.Domain;

public class ContentBlock : ValueObject
{
    private ContentBlock() { }

    public static ContentBlock New(
        Guid contentId,
        string source,
        ContentBodyType bodyType,
        string textContent,
        bool hidden = false
    )
    {
        return new ContentBlock
        {
            ContentId = contentId,
            Source = source,
            Hidden = hidden,
            BodyType = bodyType,
            TextContent = textContent
        };
    }
    
    public Guid ContentId { get; private set; }
    
    public string Source { get; private set; }
    
    public bool Deleted { get; private set; }
    
    public bool Hidden { get; private set; }
    
    public ContentBodyType BodyType { get; private set; }
    
    public string TextContent { get; private set; }
    
    public int OrderNum { get; private set; }
    
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return ContentId;
        yield return TextContent;
        yield return Source;
        yield return BodyType;
        yield return OrderNum;
        yield return Deleted;
    }
}