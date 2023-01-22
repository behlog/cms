namespace Behlog.Cms.Validations;

public class TagErrorCodes
{
    public static string TitleIsNull = "tag.title.null";
    public static string TitleMaxLen = "tag.title.maxlen";
    public static string SlugIsNull = "tag.slug.null";
    public static string SlugMaxLen = "tag.slug.maxlen";

    public static string? GetMessage(string errorCode)
    {
        return ValidationMessages.ResourceManager.GetString(errorCode);
    }
}