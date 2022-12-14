using Behlog.Core;

namespace Behlog.Cms.Domain;

public enum WebsiteStatusEnum
{
    Deleted = -1,
    Offline = 0,
    Online = 1,
    UnderConstruction = 2,
    Closed = 13
}


public class WebsiteStatus : Enumeration
{
    public WebsiteStatus(int id, string name, string title = "") 
        : base(id, name, title)
    {
    }


    public static WebsiteStatus Find(int id) => FromValue<WebsiteStatus>(id);

    public static WebsiteStatus Deleted
        => new WebsiteStatus(-1, nameof(Deleted));

    public static WebsiteStatus Offline
        => new WebsiteStatus(0, nameof(Offline));

    public static WebsiteStatus Online
        => new WebsiteStatus(1, nameof(Online));

    public static WebsiteStatus UnderConstruction
        => new WebsiteStatus(2, nameof(UnderConstruction));

    public static WebsiteStatus Closed
        => new WebsiteStatus(13, nameof(Closed));
}