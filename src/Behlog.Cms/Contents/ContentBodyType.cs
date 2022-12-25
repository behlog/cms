using Behlog.Core;

namespace Behlog.Cms.Domain;

public enum ContentBodyTypeEnum
{
    Text = 0,
    HTML = 1,
    Markdown = 2
}

public class ContentBodyType : Enumeration 
{

    public ContentBodyType(int id, string name, string title = "")
        :base(id, name, title)
    {        
    }

    public static ContentBodyType Text 
        => new ContentBodyType(0, nameof(Text));

    public static ContentBodyType HTML 
        => new ContentBodyType(1, nameof(HTML));

    public static ContentBodyType Markdown 
        => new ContentBodyType(2, nameof(Markdown));

    public static ContentBodyType Find(int id) => FromValue<ContentBodyType>(id);
}