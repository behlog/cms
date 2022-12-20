using Behlog.Cms.Resources;

namespace Behlog.Cms.Validations;

public static class ComponentErrorCodes
{
    public static string NameIsNull = "component.name.null";
    public static string TitleIsNull = "component.title.null";
    public static string CategoryIsNull = "component.category.null";
    public static string ComponentTypeIsNull = "component.type.null";
    public static string NameMaxLen = "component.name.maxlen";
    public static string TitleMaxLen = "component.title.maxlen";
    public static string CategoryMaxLen = "component.category.maxlen";
    public static string ComponentTypeMaxLen = "component.type.maxlen";
    public static string DescriptionMaxLen = "component.desc.maxlen";
    public static string AuthorMaxLen = "component.author.maxlen";
    public static string AuthorEmailMaxLen = "component.author.email.maxlen";
    public static string AuthorEmailFormat = "component.author.email.format";
    public static string KeywordsMaxLen = "component.keywords.maxlen";
    public static string ViewPathMaxLen = "component.view.maxlen";
    
    public static string? GetMessage(string errorCode)
    {
        return ValidationMessages.ResourceManager.GetString(errorCode);
    }
}