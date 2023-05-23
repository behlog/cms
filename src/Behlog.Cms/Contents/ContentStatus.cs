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

public static class ContentStatusExtensions
{

    public static string ToDisplay(this ContentStatusEnum status) 
        => status switch
        {
            ContentStatusEnum.Deleted => "Deleted", //TODO : Read from resource
            ContentStatusEnum.Draft => "Draft",
            ContentStatusEnum.Planned => "Planned",
            ContentStatusEnum.Published => "Published",
            _ => "Unknown"
        };
}
