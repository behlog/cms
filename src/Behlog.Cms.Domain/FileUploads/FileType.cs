using Behlog.Core;

namespace Behlog.Cms.Domain;

public class FileType : Enumeration
{
    public FileType(int id, string name, string title = "") 
        : base(id, name, title)
    {
    }

    public static FileType Common
        => new FileType(0, nameof(Common));

    public static FileType Image
        => new FileType(1, nameof(Image));

    public static FileType Video
        => new FileType(2, nameof(Video));

    public static FileType Audio
        => new FileType(3, nameof(Audio));

    public static FileType Document
        => new FileType(4, nameof(Document));

    public static FileType Downloads
        => new FileType(4, nameof(Downloads));
}