using Behlog.Core;

namespace Behlog.Cms.Domain;

public class WebpageStatus : Enumeration
{
    public WebpageStatus(int id, string name, string title = "") : base(id, name, title)
    {
    }

    public static WebpageStatus Find(int id) => FromValue<WebpageStatus>(id);

    public static WebpageStatus Deleted =
        new WebpageStatus(-1, nameof(Deleted));

    public static WebpageStatus Created =
        new WebpageStatus(0, nameof(Created));

    public static WebpageStatus Published =
        new WebpageStatus(1, nameof(Published));

    public static WebpageStatus Disabled =
        new WebpageStatus(2, nameof(Disabled));
}