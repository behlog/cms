using Behlog.Cms.Domain;

namespace Behlog.Cms.Extensions;

public static class EnumExtensions {

    public static string GetFileTypeName(this FileTypeEnum fileType) {
        return fileType switch
        {
            FileTypeEnum.Common => "Common",
            FileTypeEnum.Image => "Image",
            FileTypeEnum.Video => "Video",
            FileTypeEnum.Audio => "Audio",
            FileTypeEnum.Document => "Document",
            FileTypeEnum.Downloads => "Downloads",
            FileTypeEnum.CompressedArchive => "CompressedArchive",
            _=> "Unknown"
        };
    }
}