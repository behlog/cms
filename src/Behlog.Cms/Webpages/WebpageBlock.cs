using Behlog.Core.Domain;

namespace Behlog.Cms.Domain;

public class WebpageBlock : ValueObject
{

    private WebpageBlock() { }

    public static WebpageBlock New(
        Guid webpageId, Guid blockId, ContentBodyType bodyType)
    {
        return new WebpageBlock
        {
            WebpageId = webpageId,
            BlockId = blockId,
            BodyType = bodyType
        };
    }

    public WebpageBlock SetSource(string source)
    {
        this.Source = source;
        return this;
    }

    public WebpageBlock SetProperties(string properties)
    {
        this.Properties = properties;
        return this;
    }
    
    public WebpageBlock SetTextContent(string textContent)
    {
        this.TextContent = textContent;
        return this;
    }

    public WebpageBlock SetOrderNum(int orderNum)
    {
        this.OrderNum = orderNum;
        return this;
    }
    
    public WebpageBlock Hide()
    {
        this.Hidden = true;
        return this;
    }


    public WebpageBlock Delete()
    {
        this.Deleted = true;
        return this;
    }

    #region props
    public int Id { get; protected set; }
    public Guid WebpageId { get; private set; }
    public Guid BlockId { get; private set; }
    public ContentBodyType BodyType { get; private set; }
    public string Source { get; private set; }
    public string Properties { get; private set; }
    public bool Deleted { get; private set; }
    public bool Hidden { get; private set; }
    public string TextContent { get; private set; }
    public int OrderNum { get; private set; }

    #endregion
    
    
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return WebpageId;
        yield return BlockId;
        yield return TextContent;
        yield return Source;
        yield return Properties;
        yield return BodyType;
        yield return OrderNum;
        yield return Deleted;
    }
}