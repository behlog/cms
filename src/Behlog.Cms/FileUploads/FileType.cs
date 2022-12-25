using Behlog.Core;

namespace Behlog.Cms.Domain;

public enum FileTypeEnum
{
    Common = 0,
    Image = 1,
    Video = 2,
    Audio = 3,
    Document = 4,
    Downloads = 5,
    CompressedArchive = 6
}

public class FileType : Enumeration
{
    public FileType(int id, string name, string title = "") 
        : base(id, name, title)
    {
    }

    public static FileType Find(int id) => FromValue<FileType>(id);
    
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
        => new FileType(5, nameof(Downloads));

    public static FileType CompressedArchive
        => new FileType(6, nameof(CompressedArchive));
}