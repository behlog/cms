namespace Behlog.Cms.Validations;

public class ContentErrorCodes
{
    public static string TitleIsNull = "content.title.null";
    public static string TitleMaxLen = "content.title.maxlen";
    public static string AuthorUserIsNull = "content.author.null";
    public static string SlugMaxLen = "content.slug.maxlen";
    public static string SummaryMaxLen = "content.summary.maxlen";
    public static string AltTitleMaxLen = "content.alt.maxlen";
    public static string PasswordMaxLen = "content.pwd.maxlen";
    public static string ViewPathMaxLen = "content.view.maxlen";
    public static string IconMaxLen = "content.icon.maxlen";

    public static string? GetMessage(string errorCode)
    {
        return "";
    }
}