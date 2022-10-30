using Behlog.Core;

namespace Behlog.Cms.Domain;

public class FileStatus : Enumeration
{
    public FileStatus(int id, string name, string title = "") 
        : base(id, name, title)
    {
    }

    public static FileStatus Deleted
        => new FileStatus(-1, nameof(Deleted));

    public static FileStatus UnAttached
        => new FileStatus(0, nameof(UnAttached));

    public static FileStatus Attached
        => new FileStatus(1, nameof(Attached));

    public static FileStatus Hidden
        => new FileStatus(2, nameof(Hidden));

    public static FileStatus Archived
        => new FileStatus(3, nameof(Archived));

}