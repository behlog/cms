namespace Behlog.Cms.Commands;


public class UpdateCommentCommand : IBehlogCommand<CommandResult>
{
    public UpdateCommentCommand(
        Guid id, string title, string body, 
        ContentBodyTypeEnum bodyType, string email, 
        string webUrl)
    {
        Id = id;
        Title = title;
        Body = body;
        BodyType = bodyType;
        Email = email;
        WebUrl = webUrl;
    }
    
    public Guid Id { get; }
    public string Title { get; }
    public string Body { get; }
    public ContentBodyTypeEnum BodyType { get; }
    public string Email { get; }
    public string WebUrl { get; }
}