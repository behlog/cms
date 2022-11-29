namespace Behlog.Cms;

public static class ContentTypes
{

    public const string Page = "page";
    
    public const string Blog = "blog";

    public const string News = "news";

    public const string PhotoGallery = "gallery";

    public const string Video = "video";

    public const string Link = "link";


    public static string[] All
        => new[]
        {
            Page, Blog, News, PhotoGallery, Video, Link
        };

    public static Dictionary<string, string> PersianNames
        => new Dictionary<string, string>
        {
            { Page, "صفحه" },
            { Blog, "وبلاگ" },
            { News, "اخبار" },
            { PhotoGallery, "عکس" },
            { Video, "ویدئو" },
            { Link, "لینک" }
        };
}