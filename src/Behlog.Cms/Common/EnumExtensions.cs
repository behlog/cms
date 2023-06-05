namespace Behlog.Cms.Extensions;

public static class EnumExtensions
{

    public static string GetFileTypeName(this FileTypeEnum fileType) => fileType switch {
        FileTypeEnum.Common => "Common",
        FileTypeEnum.Image => "Image",
        FileTypeEnum.Video => "Video",
        FileTypeEnum.Audio => "Audio",
        FileTypeEnum.Document => "Document",
        FileTypeEnum.Downloads => "Downloads",
        FileTypeEnum.CompressedArchive => "CompressedArchive",
        _ => "Unknown"
    };

    public static string ToDisplay(this FileTypeEnum fileType) => fileType switch {
        FileTypeEnum.Common => EnumText.FileType_Common,
        FileTypeEnum.Image => EnumText.FileType_Image,
        FileTypeEnum.Video => EnumText.FileType_Video,
        FileTypeEnum.Audio => EnumText.FileType_Audio,
        FileTypeEnum.Document => EnumText.FileType_Document,
        FileTypeEnum.Downloads => EnumText.FileType_Downloads,
        FileTypeEnum.CompressedArchive => EnumText.FileType_CompressedArchive,
        _ => EnumText.FileType_Unknown
    };

    public static string ToDisplay(this ContentStatusEnum status)
        => status switch {
            ContentStatusEnum.Deleted => EnumText.Deleted, 
            ContentStatusEnum.Draft => EnumText.Draft,
            ContentStatusEnum.Planned => EnumText.Content_Planned,
            ContentStatusEnum.Published => EnumText.Content_Published,
            _ => EnumText.Unknown
        };

    public static string ToDisplay(this WebsiteStatus status) =>
        status switch {
            WebsiteStatus.Deleted => EnumText.Deleted,
            WebsiteStatus.Offline => EnumText.Website_Offline,
            WebsiteStatus.Online => EnumText.Website_Online,
            WebsiteStatus.UnderConstruction => EnumText.UnderConstruction,
            WebsiteStatus.Closed => EnumText.Website_Closed,
            _=> EnumText.Unknown
        };

    public static string ToDisplay(FileUploadStatus status) =>
        status switch {
            FileUploadStatus.Deleted => EnumText.Deleted,
            FileUploadStatus.Created => EnumText.Created,
            FileUploadStatus.Attached => EnumText.File_Attached,
            FileUploadStatus.Archived => EnumText.Archived,
            FileUploadStatus.Hidden => EnumText.Hidden,
            _=> EnumText.Unknown
        };

    public static string ToDisplay(this CommentStatusEnum status) =>
        status switch {
            CommentStatusEnum.Spam => EnumText.Comment_Spam,
            CommentStatusEnum.Approved => EnumText.Comment_Approved,
            CommentStatusEnum.Deleted => EnumText.Deleted,
            CommentStatusEnum.Created => EnumText.Created,
            CommentStatusEnum.Blocked => EnumText.Comment_Blocked,
            CommentStatusEnum.Rejected => EnumText.Comment_Rejected,
            _=> EnumText.Unknown
        };
    
}