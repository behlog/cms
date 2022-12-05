using Behlog.Core.Domain;

namespace Behlog.Cms.Domain;

public class ContentBlock : ValueObject
{
    private ContentBlock() { }

    public static ContentBlock New(
        Guid contentId,
        Guid blockId,
        ContentBodyType bodyType
    )
    {
        return new ContentBlock
        {
            ContentId = contentId,
            BodyType = bodyType,
            BlockId = blockId
        };
    }
    
    public long Id { get; private set; }

    public ContentBlock Hide()
    {
        this.Hidden = true;
        return this;
    }

    public ContentBlock SetSource(string source)
    {
        this.Source = source;
        return this;
    }

    public ContentBlock SetProperties(string properties)
    {
        this.Properties = properties;
        return this;
    }

    public ContentBlock SetTextContent(string textContent)
    {
        this.TextContent = textContent;
        return this;
    }

    public ContentBlock SetOrderNum(int orderNum)
    {
        this.OrderNum = orderNum;
        return this;
    }

    public ContentBlock Delete()
    {
        this.Deleted = true;
        return this;
    }
    
    public Guid ContentId { get; private set; }
    
    public Guid BlockId { get; private set; }
    
    public string Source { get; private set; }
    
    public string? Properties { get; private set; }
    
    public bool Deleted { get; private set; }
    
    public bool Hidden { get; private set; }
    
    public ContentBodyType BodyType { get; private set; }
    
    public string? TextContent { get; private set; }
    
    public int OrderNum { get; private set; }
    
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return ContentId;
        yield return BlockId;
        yield return TextContent!;
        yield return Source;
        yield return Properties!;
        yield return BodyType;
        yield return OrderNum;
        yield return Deleted;
    }
}