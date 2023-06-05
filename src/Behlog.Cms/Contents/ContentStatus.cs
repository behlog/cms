using Behlog.Core;
using Behlog.Cms.Resources;

namespace Behlog.Cms.Domain;

public enum ContentStatusEnum
{
    Deleted = -1,
    Draft = 0,
    Published = 1,
    Planned = 2
}
