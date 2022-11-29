using Behlog.Cms.Resources;

namespace Behlog.Cms.Errors;


public static class ContentErrorCodes
{
    public static string TitleIsNull = "content.title.null";
    public static string TitleMaxLen = "content.title.maxlen";
    public static string SlugMaxLen = "content.slug.maxlen";
    public static string SummaryMaxLen = "content.summary.maxlen";
    public static string AltTitleMaxLen = "content.alt.maxlen";
    public static string PasswordMaxLen = "content.pwd.maxlen";
    public static string ViewPathMaxLen = "content.view.maxlen";
    public static string IconMaxLen = "content.icon.maxlen";
    

    public static string? GetMessage(string errorCode)
    {
        return ValidationMessages.ResourceManager.GetString(errorCode);
    }
}


public static class MetaErrorCodes
{
    public static string KeyIsNull = "meta.key.null";
    public static string TitleMaxLen = "meta.title.maxlen";
    public static string ValueMaxLen = "meta.value.maxlen";
    public static string CategoryMaxLen = "meta.cat.maxlen";
    public static string DescriptionMaxLen = "meta.desc.maxlen";


    public static string? GetMessage(string errorCode)
    {
        return ValidationMessages.ResourceManager.GetString(errorCode);
    }
}


public static class LikeErrorCodes
{
    public static string SessionIdMaxLen = "like.session.maxlen";
    public static string UserIdMaxLen = "like.user.maxlen";
    public static string IpAddressMaxLen = "like.ip.maxlen";
    
    public static string? GetMessage(string errorCode)
    {
        return ValidationMessages.ResourceManager.GetString(errorCode);
    }
}


public static class ContentFileErrorCodes
{
    public static string FileIdInvalid = "content.file.id.invalid";
    public static string FileNameIsNull = "content.file.name.null";
    public static string FileNameMaxLen = "content.file.name.maxlen";
    public static string TitleMaxLen = "content.title.maxlen";
    public static string DescriptionMaxLen = "content.desc.maxlen";
    
    
    public static string? GetMessage(string errorCode)
    {
        return ValidationMessages.ResourceManager.GetString(errorCode);
    }
}

