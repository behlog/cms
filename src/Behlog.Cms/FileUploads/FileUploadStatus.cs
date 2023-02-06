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
