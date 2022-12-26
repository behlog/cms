using Behlog.Cms.Resources;

namespace Behlog.Cms.Validations;

public static class CommentErrorCodes
{
    public static string TitleMaxLen = "comment.maxlen";
    public static string BodyIsNull = "comment.body.null";
    public static string BodyMaxLen = "comment.body.maxlen";
    public static string EmailMaxLen = "comment.email.maxlen";
    public static string WebUrlMaxLen = "comment.website.maxlen";
    public static string AuthorNameMaxLen = "comment.author.maxlen";
    
    
    public static string? GetMessage(string errorCode)
    {
        return ValidationMessages.ResourceManager.GetString(errorCode);
    }
}