namespace Behlog.Cms;

public class CreateCommentArg 
{
    public string Title { get; }
    public string Body { get; }
    public ContentBodyType BodyType { get; }
    public string Email { get; }
    public string WebUrl { get; }
    public string AuthorName { get; }
}