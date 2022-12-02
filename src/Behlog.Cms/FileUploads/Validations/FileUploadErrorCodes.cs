using Behlog.Cms.Resources;

namespace Behlog.Cms.Errors;

public static class FileUploadErrorCodes
{
    public static string FileIsNull = "file.file.null";
    public static string InvalidWebsiteId = "file.website.id.invalid";
    public static string TitleMaxLen = "file.title.maxlen";
    public static string FilePathMaxLen = "file.path.maxlen";
    public static string AltFilePathMaxLen = "file.altpath.maxlen";
    public static string AltTitleMaxLen = "file.alt.maxlen";
    public static string ExtensionMaxLen = "file.ext.maxlen";
    public static string UrlMaxLen = "file.url.maxlen";
    public static string UrlRequired = "file.url.null";
    public static string DescriptionMaxLen = "file.desc.maxlen";
    
    
    public static string? GetMessage(string errorCode)
    {
        return ValidationMessages.ResourceManager.GetString(errorCode);
    }
}