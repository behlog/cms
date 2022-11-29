using Behlog.Cms.Resources;

namespace Behlog.Cms.Validations;

public static class ContentCategoryErrorCodes
{

    public static string TitleIsNull = "contentcat.title.null";
    public static string TitleMaxLen = "contentcat.title.maxlen";
    public static string AltTitleMaxLen = "contentcat.alttitle.maxlen";
    public static string SlugIsNull = "contentcat.slug.null";
    public static string SlugMaxLen = "contentcat.slug.maxlen";
    public static string DescriptionMaxLen = "contentcat.desc.maxlen";


    public static string? GetMessage(string errorCode)
    {
        return ValidationMessages.ResourceManager.GetString(errorCode);
    }
}