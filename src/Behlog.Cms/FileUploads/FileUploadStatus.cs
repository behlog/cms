using Behlog.Core;

namespace Behlog.Cms.Domain;

public enum FileUploadStatusEnum
{
    Deleted = -1,
    Created = 0,
    Attached = 1,
    Hidden = 2,
    Archived = 3
}

public class FileUploadStatus : Enumeration
{
    public FileUploadStatus(int id, string name, string title = "") 
        : base(id, name, title)
    {
    }

    public static FileUploadStatus Deleted
        => new FileUploadStatus(-1, nameof(Deleted));

    public static FileUploadStatus Created
        => new FileUploadStatus(0, nameof(Created));

    public static FileUploadStatus Attached
        => new FileUploadStatus(1, nameof(Attached));

    public static FileUploadStatus Hidden
        => new FileUploadStatus(2, nameof(Hidden));

    public static FileUploadStatus Archived
        => new FileUploadStatus(3, nameof(Archived));

    public static FileUploadStatus Find(int id) => FromValue<FileUploadStatus>(id);
}