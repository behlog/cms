using Behlog.Cms.Resources;

namespace Behlog.Cms.Handlers;

public static class WebsiteErrorCodes
{

    public static string NameIsNull = "website.name.null";
    public static string NameMaxLen = "website.name.maxlen";
    public static string OwnerUserNull = "website.owneruser.null";
    public static string TitleIsNull = "website.title.null";
    public static string TitleMaxLen = "website.title.maxlen";
    public static string DescriptionMaxLen = "website.desc.maxlen";
    public static string KeywordsMaxLen = "website.keywords.maxlen";
    public static string UrlMaxLen = "website.url.maxlen";
    public static string InvalidStatus = "webiste.status.invalid";
    public static string EmailFormat = "website.email.format";
    public static string CopyrightMaxLen = "website.cpr.maxlen";

    public static string CreateStoreFatal = "website.create.fatal";
    public static string UpdateStoreFatal = "website.update.fatal";

    public static string? GetMessage(string errorCode)
    {
        return ValidationMessages.ResourceManager.GetString(errorCode);
    }
}