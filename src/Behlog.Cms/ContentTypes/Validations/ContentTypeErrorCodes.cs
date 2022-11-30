using Behlog.Cms.Resources;

namespace Behlog.Cms.Errors;


public static class ContentTypeErrorCodes
{
    public static string SystemNameIsNull = "ctype.sysname.null";
    public static string TitleIsNull = "ctype.sysname.null";
    public static string SlugIsNull = "ctype.slug.null";
    public static string LangIsNull = "ctype.lang.null";
    public static string SystemNameMaxLen = "ctype.sysname.maxlen";
    public static string TitleMaxLen = "ctype.title.maxlen";
    public static string SlugMaxLen = "ctype.slug.maxlen";
    public static string DescriptionMaxLen = "ctype.desc.maxlen";
    
    
    public static string? GetMessage(string errorCode)
    {
        return ValidationMessages.ResourceManager.GetString(errorCode);
    }
}